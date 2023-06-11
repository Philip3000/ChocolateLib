using CharlottesStockApi.Repository;
using ChocolateLib;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CharlottesStockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EasterEggsController : ControllerBase
    {
        private readonly EasterEggsRepository _repository;
        public EasterEggsController(EasterEggsRepository repository)
        {
            _repository = repository;
        }
        // GET: api/<EasterEggsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<EasterEgg>> Get()
        {
            List<EasterEgg> easterEggs = _repository.Get();
            if (easterEggs == null) return NotFound("No Easter Eggs exist");
            return Ok(easterEggs);
        }

        // GET api/<EasterEggsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{productNo}")]
        public ActionResult<EasterEgg> Get(int productNo)
        {
            EasterEgg easterEgg = _repository.GetByProductNo(productNo);
            if (easterEgg == null)
                return NotFound("No Easter Egg found with product number: " + productNo);
            else
                return Ok(easterEgg);
        }
        // GET api/<EasterEggsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("lowStock/{stockLevel}")]
        public ActionResult<List<EasterEgg>> GetLowStock(int stockLevel)
        {
            List<EasterEgg> easterEggStock = _repository.GetLowStock(stockLevel);
            if (easterEggStock == null) return NotFound("No products with lower stock than" + stockLevel);
            return Ok(easterEggStock);
        }

        // PUT api/<EasterEggsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut]
        public ActionResult<EasterEgg> Put([FromBody] EasterEgg value)
        {
            try
            {
                EasterEgg updatedEasterEgg = _repository.Update(value);
                return Ok(updatedEasterEgg);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
