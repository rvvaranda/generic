using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Generic.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace Generic.Api.Controllers
{
    public abstract class BaseApiController<MODEL, DTO, BO> : ControllerBase
        where MODEL : class where DTO : class, new() where BO : class, new()
    {
        GenericLogger logger = new GenericLogger();

        public IActionResult CreateRoute(string routeName)
        {
            return CreatedAtRoute(routeName, new object());
        }

        [HttpGet("all")]
        public IActionResult ListarTodos()
        {
            try
            {
                StringValues tokenAcesso = "";

                var obj = RetornarInstancia();
                var method = obj.GetType().GetMethod("ListarBO");

                var list = method.Invoke(obj, null);
                var rslt = Mapper.Map<IList<DTO>>(list);

                return Ok(rslt);

            }
            catch (Exception ex)
            {
                logger.ArquivoLog("ListarTodos", ex, "Record");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public IActionResult ListarAtivos([FromQuery] int? q)
        {
            try
            {
                StringValues tokenAcesso = "";

                var obj = RetornarInstancia();
                var method = obj.GetType().GetMethod(q == 1 ? "ListarAtivoBO" : "ListarInativoBO");


                var list = method.Invoke(obj, null);
                var rslt = Mapper.Map<IList<DTO>>(list);


                return Ok(rslt);

            }
            catch (Exception ex)
            {
                logger.ArquivoLog("ListarAtivos", ex, "Record");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpGet("{value}/itens")]
        public virtual IActionResult FiltrarItem([FromQuery] string field, string value)
        {
            try
            {
                StringValues tokenAcesso = "";

                var obj = RetornarInstancia();
                var method = obj.GetType().GetMethod("FiltrarBO");

                var param = Expression.Parameter(typeof(MODEL), "e");
                var field_filter = Expression.Property(param, field);
                var constant = Expression.Constant(value);
                var filter = Expression.Equal(field_filter, constant);

                var expression = Expression.Lambda<Func<MODEL, bool>>(filter, param);
                
                var rslt = method.Invoke(obj, new object[] { expression });

                if (rslt == null)
                {
                    return NotFound();
                }

                return Ok(Mapper.Map<IList<DTO>>(rslt));

            }
            catch (Exception ex)
            {
                logger.ArquivoLog("RetornarPorGuid", ex, "Record");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{guid}")]
        public virtual IActionResult RetornarPorGuid(string guid)
        {
            try
            {
                StringValues tokenAcesso = "";

                var obj = RetornarInstancia();
                var method = obj.GetType().GetMethod("RetornarPorGuidBO");

                var rslt = method.Invoke(obj, new object[] { guid });

                if (rslt == null)
                {
                    return NotFound();
                }

                return Ok(Mapper.Map<DTO>(rslt));

            }
            catch (Exception ex)
            {
                logger.ArquivoLog("RetornarPorGuid", ex, "Record");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public virtual IActionResult InserirDados([FromBody]DTO dados)
        {
            try
            {
                var obj = RetornarInstancia(); //breakpoint
                var method = obj.GetType().GetMethod("InserirBO");
                var dtoObj = dados.GetType();
                
                var data = Mapper.Map<MODEL>(dados);
                var rslt = method.Invoke(obj, new object[] { data });

                var propInfo = dtoObj.GetProperty("Guid");

                propInfo.SetValue(dados, rslt);

                if (!rslt.Equals(""))
                {
                    return CreatedAtAction("RetornarPorGuid", new { guid = rslt }, dados);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("InserirDados", ex, "Record");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpPut("{guid}")]
        public IActionResult EditarDados(string guid, [FromBody] DTO dados)
        {
            try
            {
                var dtoObj = dados.GetType();
                var propInfo = dtoObj.GetProperty("Guid");
                var idObj = propInfo.GetValue(dados, null);

                if (!guid.Equals((string)idObj))
                {
                    return BadRequest();
                }

                var obj = RetornarInstancia();
                var method = obj.GetType().GetMethod("EditarBO");

                var data = Mapper.Map<MODEL>(dados);
                var rslt = method.Invoke(obj, new object[] { data });

                if ((bool)rslt)
                {
                    return NoContent();
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("EditarDados", ex, "Record");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{guid}")]
        public IActionResult RemoverDados(string guid)
        {
            try
            {

                var obj = RetornarInstancia();
                var methodGet = obj.GetType().GetMethod("RetornarPorGuidBO");

                var dados = methodGet.Invoke(obj, new object[] { guid });

                if (dados == null)
                {
                    return NotFound();
                }

                var method = obj.GetType().GetMethod("ExcluirBO");
                var data = Mapper.Map<MODEL>(dados);

                var rslt = method.Invoke(obj, new object[] { data });

                if ((bool)rslt)
                {
                    return new NoContentResult();
                }

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("RemoverDados", ex, "Record");
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        protected BO RetornarInstancia()
        {
            try
            {
                return (BO)Activator.CreateInstance(typeof(BO));
            }
            catch (Exception ex)
            {
                logger.ArquivoLog("RetornarInstancia", ex, "Record");
                return null;
            }
        }
    }
}