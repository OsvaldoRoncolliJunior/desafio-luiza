

using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IManterPedidoLogisticaService _service;

    public UserController(IManterPedidoLogisticaService service)
    {
        _service = service;
    }

    // Método GET para retornar uma lista de usuários
    [HttpGet("users")]
    public IActionResult Users()
    {
        List<User> users = _service.getAllPedidosUsuarios();
        return Ok(users);
    }

    [HttpGet("userId")]
    public IActionResult UsersId(int id)
    {
        User user = _service.getPedidosUsuarioById(id);
        return Ok(user);
    }

    [HttpGet("userByDatas")]
    public IActionResult UsersByDatas(DateTime dataInicio, DateTime dataFim)
    {
        List<User> users = _service.getAllPedidosUsuariosByDatas(dataInicio, dataFim);
        return Ok(users);
    }

    [HttpPost("UploadFile")]
    public IActionResult UploadFile(IFormFile file)
    {
        // Verifica se o arquivo foi enviado
        if (file == null || file.Length == 0)
        {
            return BadRequest("Nenhum arquivo foi enviado.");
        }

        // Processar o arquivo
        try
        {
            _service.postPedidosUsuarioByFile(file.OpenReadStream());

            return Ok();
        }
        catch (IOException ex)
        {
            return StatusCode(500, "Erro ao processar o arquivo: " + ex.Message);
        }
    }
}
    