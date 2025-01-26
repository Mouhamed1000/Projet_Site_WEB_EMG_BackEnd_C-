using EMG_MED1000_BACKEND.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class MarqueController : ControllerBase
{
    private readonly MarqueService _marqueService;

    //Création d'un constructeur afin d'initialiser notre objet _marqueService d'accès à notre classe MarqueService
    public MarqueController(MarqueService marqueService)
    {
        _marqueService = marqueService;
    }

    //A partir de notre objet _marqueService, on aura acces a nos fonctionnalités métiers

    [HttpGet]
    public async Task<IActionResult> GetAllMarques ()
    {
        var marque = await _marqueService.GetAllMarquesAsync();
        return Ok(marque);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMarque([FromBody] Marque marque)
    {
        //Création de la marque via notre service en lui fournissant les différents paramètres
        var createdMarque = await _marqueService.CreateMarque(marque.NomMarq, marque.ListModele);

        return StatusCode(201, createdMarque);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMarque(int id)
    {
        var success = await _marqueService.DeleteMarque(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

}