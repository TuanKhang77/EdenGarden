using EdenGarden_API.Data;
using EdenGarden_API.Models.Request;
using EdenGarden_API.Services.Interfaces;
using EdenGarden_API.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using EdenGarden_API.Services;
using EdenGarden_API.Helper;
using EdenGarden_API.Models.Response;

namespace EdenGarden_API.Controllers
{
    public class BookingController : BaseController
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingService _bookingService;
        private readonly WebSocketService _websocketService;


        public string returnUrl = $"https://localhost:5001/api/booking/PaymentCallback";

        public BookingController(ILogger<BookingController> logger, 
            DataContext context, 
            //IClientService clientService, 
            IServiceService serviceService,
            IBookingService bookingService,
            WebSocketService webSocketService)
        {
            _logger = logger;
            //_clientService = clientService;
            _bookingService = bookingService;
            _websocketService = webSocketService;
        }

        //[HttpPost]
        //[AllowAnonymous]
        //public IActionResult CreateBooking2([FromBody] CreateBookingRequest2 createdBookingRequest)
        //{
        //    string hostName = System.Net.Dns.GetHostName();
        //    string clientIPAddress = System.Net.Dns.GetHostAddresses(hostName).GetValue(0).ToString();

        //    VnPayLibrary pay = new VnPayLibrary();

        //    pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
        //    pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
        //    pay.AddRequestData("vnp_TmnCode", "MQQ3M5E6"); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
        //    pay.AddRequestData("vnp_Amount", (createdBookingRequest.amount * 100).ToString()); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000

        //    pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
        //    pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
        //    pay.AddRequestData("vnp_IpAddr", clientIPAddress); //Địa chỉ IP của khách hàng thực hiện giao dịch
        //    pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
        //    pay.AddRequestData("vnp_OrderInfo", "Thanh toan don dat phong"); //Thông tin mô tả nội dung thanh toán
        //    pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
        //    pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
        //    pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            
        //    string paymentUrl = pay.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", "7B5BF0DGWPXWHLH6WJPHDZ6A9VUPTCYZ");


        //    return Ok(paymentUrl);
        //}

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateBooking([FromBody] CreateBookingRequest2 createdBookingRequest)
        {
            Response response = _bookingService.CreateBook(createdBookingRequest);
            if(!response.Success)
            {
                return BadRequest(response);
            }

            string hostName = System.Net.Dns.GetHostName();
            string clientIPAddress = System.Net.Dns.GetHostAddresses(hostName).GetValue(0).ToString();

            VnPayLibrary pay = new VnPayLibrary();

            Response69<Booking> response69 = (Response69<Booking>)response;

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", "MQQ3M5E6"); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", ((response69.Data.TotalAmount /2).ToString()) + "00"); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", clientIPAddress); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan don dat phong"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn
            
            //Console.WriteLine(response69.Data.TotalAmount.ToString());
            string paymentUrl = pay.CreateRequestUrl("https://sandbox.vnpayment.vn/paymentv2/vpcpay.html", "7B5BF0DGWPXWHLH6WJPHDZ6A9VUPTCYZ");
            
            CreateBookingResponse createBookingResponse = new CreateBookingResponse
            {
                PaymentUrl = paymentUrl,
                Booking = response69.Data

            };
            return Ok(createBookingResponse);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = _bookingService.PaymentExecute(Request.Query);
            await _websocketService.SendMessageAsync(response.VnPayResponseCode);
            // Thêm dòng dưới đây để đóng cửa sổ thanh toán khi callback được gọi
            //await _websocketService.SendMessageAsync("close");
            string content;
            if (response.VnPayResponseCode == "00")
            {
                content = "<html lang='vi'><body>THANH TOÁN THÀNH CÔNG, VUI LÒNG TẮT TRÌNH DUYỆT VÀ NHẬN THÔNG BÁO CHI TIẾT ĐƠN ĐẶT</body></html>";
            }
            else
            {
                content = "<html lang='vi'><body>THANH TOÁN KHÔNG THÀNH CÔNG, VUI LÒNG TẮT TRÌNH DUYỆT VÀ ĐẶT LẠI</body></html>";
            }
            return new ContentResult
            {
                Content = content,
                ContentType = "text/html; charset=utf-8"
            };
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SaveBooking([FromBody] Booking booking)
        {
            var res = _bookingService.SaveBookingAfterPayment(booking);
            _logger.LogInformation("Object updated successfully.");
            if (res)
                return Ok();
            return BadRequest();
        }

        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            try
            {
                //string userId = User.Claims.First().Value;
                var rooms = _bookingService.GetAll().ToList();
                _logger.LogInformation("Retrieved all objects successfully");
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all objects");
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        [AllowAnonymous] 
        public IActionResult GetAllRoomAvailable([FromBody] FindRoomTypeRequest findRoomTypeRequest)
        {
            try
            {
                //string userId = User.Claims.First().Value;
                var response = _bookingService.GetRoomAvailable(findRoomTypeRequest);
                _logger.LogInformation("Retrieved all objects successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all objects");
                return StatusCode(500, ex.Message);
            }
        }


        
    }
}
