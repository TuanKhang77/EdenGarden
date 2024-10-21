using EdenGarden_API.Data;
using EdenGarden_API.Helper;
using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Request;
using EdenGarden_API.Models.Response;
using EdenGarden_API.Repository.Interfaces;
using EdenGarden_API.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EdenGarden_API.Services
{
    public class BookingService : IBookingService
    {
        private readonly DataContext _dataContext;
        private readonly ICommonRepository _respository;

        public BookingService(DataContext dataContext, ICommonRepository respository)
        {
            _dataContext = dataContext;
            _respository = respository;

        }

        public Response CreateBook(CreateBookingRequest2 bookingReq)
        {
            //convert sang datetime
            DateTime BeginDateTime = bookingReq.BeginDate.ToDateTime(TimeOnly.MinValue);
            BeginDateTime = BeginDateTime.Date.AddHours(12);
            DateTime EndDateTime = bookingReq.EndDate.ToDateTime(TimeOnly.MinValue);
            EndDateTime = EndDateTime.Date.AddHours(11);


            //xử lý ở frontend
            if (bookingReq.BeginDate.CompareTo(DateOnly.FromDateTime(DateTime.Now)) <= 0 ||
                bookingReq.BeginDate.CompareTo(bookingReq.EndDate) > 0)
            {
                return new Response
                {
                    Success = false,
                    Message = "Unable to create booking1"
                };
            }

            //tìm các room đúng loại roomtype và đúng điều kiện rồi gán vào rooms
            //float totalAmount = 0;
            List<Room> rooms = new List<Room>();
            foreach (var bookedRoomRequest in bookingReq.RoomRequests)
            {
                List<Room> roomsWithSameType = _dataContext.Room
                    .Where(r => r.RoomTypeId == bookedRoomRequest.RoomTypeId &&
                            !_dataContext.BookedRoom.Any(br =>
                             br.Room.Id == r.Id &&
                             !(BeginDateTime >= br.Checkout || EndDateTime <= br.Checkin)))
                    .OrderBy(r => r.Id)
                    .Take(bookedRoomRequest.Quantity)
                    .Include(r => r.RoomType)
                    .ToList();



                //xử lý ở frontend
                if (roomsWithSameType.Count < bookedRoomRequest.Quantity)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Unable to create booking2",
                    };
                }

                roomsWithSameType.ForEach(room => rooms.Add(room));
            }


            /*SPLIT HERE*/
            List<Service> services = _dataContext.Service.Where(s => bookingReq.ServiceIds.Contains(s.Id)).ToList();
            //foreach (var service in services)
            //{
            //    totalAmount += service.Price.GetValueOrDefault(0);
            //}

            //tạo mới booking
            Booking booking = new Booking
            {
                Bookday = DateTime.Now,
                NumOfPeople = bookingReq.NumberOfPeople,
                UsedService = services,
                CreatedDate = DateTime.Now,
            };

            //tạo mới client
            booking.client = bookingReq.BookedClient;
            booking.client.CreatedDate = DateTime.Now;


            //tạo mới bookedroom
            List<BookedRoom> bookedRooms = new List<BookedRoom>();
            rooms.ForEach(r =>
            {
                BookedRoom bookedRoom = new BookedRoom
                {
                    Checkin = BeginDateTime,
                    Checkout = EndDateTime,
                    IsChecked = false,
                    Room = r,
                    CreatedDate = DateTime.Now,
                };
                bookedRooms.Add(bookedRoom);
                //totalAmount += r.RoomType.Price.GetValueOrDefault(0);
            });


            booking.BookedRooms = bookedRooms;
            booking.TotalAmount = bookingReq.amount;
            

            //tạo mới bill
            booking.Bill = new Bill
            {
                TotalPrice = bookingReq.amount,
                FirstPay = bookingReq.amount / 2,
                Status = "Cọc",
                PaymentType = "VNPay",
                CreatedDate = DateTime.Now,
            };

            //Chỉ save booking sau khi thanh toán thành công
            //_dataContext.Booking.Add(booking);

            //xử lý firstpay ở đây



            //_dataContext.SaveChanges();

            return new Response69<Booking>
            {
                Success = true,
                Message = "Create Successful",
                Data = booking
            };
        }

        public Response Delete(int id)
        {
            var arg = new
            {
                Id = id
            };
            var response = _respository.GetObjectByStore<Response>("[dbo].[Prc_BookingDelete]", arg);
            return response;
        }

        public IEnumerable<BookingFullData> GetAll()
        {
            var bookings = _respository.GetListByStore<BookingFullData>("[dbo].[Prc_BookingGetAll]", new { });

            return bookings;
        }

        public IEnumerable<RoomAvailable> GetRoomAvailable(FindRoomTypeRequest findRoomTypeRequest)
        {
            var beginDateTime = findRoomTypeRequest.BeginDate.ToDateTime(new TimeOnly(12, 0, 0));
            var endDateTime = findRoomTypeRequest.EndDate.ToDateTime(new TimeOnly(11, 0, 0));

            var temp = new
            {
                BeginDate = beginDateTime,
                EndDate = endDateTime,
                findRoomTypeRequest.NumOfPeople
            };
            var response = _respository.GetListByStore<RoomAvailable>("[dbo].[Prc_GetAvailableRoomTypes]", temp);

            return response;
        }



        public ResponseVnpay PaymentExecute(IQueryCollection collections)   
        {
            var vnpay = new VnPayLibrary();
            foreach (var (key, value) in collections)
            {
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key, value.ToString());
                }
            }

            var vnp_orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
            var vnp_TransactionId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            var vnp_SecureHash = collections.FirstOrDefault(p => p.Key == "vnp_SecureHash").Value;
            var vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            var vnp_OrderInfo = vnpay.GetResponseData("vnp_OrderInfo");
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, "7B5BF0DGWPXWHLH6WJPHDZ6A9VUPTCYZ");
            if (!checkSignature)
            {
                return new ResponseVnpay
                {
                    Success = false
                };
            }
            return new ResponseVnpay
            {
                Success = true,
                PaymentMethod = "VnPay",
                OrderDescription = vnp_OrderInfo,
                OrderId = vnp_orderId.ToString(),
                TransactionId = vnp_TransactionId.ToString(),
                Token = vnp_SecureHash,
                VnPayResponseCode = vnp_ResponseCode.ToString()
            };

        }

        public Boolean SaveBookingAfterPayment(Booking newBooking)
        {
            try
            {
                _dataContext.Booking.Add(newBooking);
                foreach (var item in newBooking.BookedRooms)
                {
                    _dataContext.Entry(item.Room).State = EntityState.Unchanged;
                }
                foreach (var item in newBooking.UsedService)
                {
                    _dataContext.Entry(item).State = EntityState.Unchanged;
                }

                _dataContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
