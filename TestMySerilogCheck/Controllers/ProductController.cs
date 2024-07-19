using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestMySerilogCheck.Models;
using MySerilogTestWithBot;
using MySerilogLog;


namespace TestMySerilogCheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private Models.ApplicationContext _context;
        private MySerilogWithBotClass _logger;
       

        public ProductController(Models.ApplicationContext context, MySerilogWithBotClass logger)
        {
            _context = context;
            _logger = logger;
           
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.Information("Get request to /api/Product", "Test");
            //_telegramBotService.SendMessageToAllUsers("test to all users");

            return Ok(_context.Products.ToList());
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
          
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok("Product added with Id: " + product.Id);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Product already exits");
                return BadRequest("Product already exits");
            }
         
        } 
    }
}
