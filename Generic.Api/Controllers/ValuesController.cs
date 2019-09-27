using Generic.Bo;
using Generic.Dto;
using Generic.Model;
using Microsoft.AspNetCore.Mvc;

namespace Generic.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ValuesController : BaseApiController<Value, ValueDto, ValuesBo>
    {

    }
}