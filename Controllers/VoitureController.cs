using EMG_MED1000_BACKEND.Entities;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class VoitureController : ControllerBase
{
    private readonly VoitureService _voitureService;

    //Création d'un constructeur afin d'initialiser notre objet _voitureService d'accès à notre classe VoitureService
    public VoitureController(VoitureService voitureService)
    {
        _voitureService = voitureService;
    }

    //A partir de notre objet _voitureService, on aura acces a nos fonctionnalités métiers

    [HttpGet]
    public async Task<IActionResult> GetAllVoitures ()
    {
        var voitures = await _voitureService.GetAllVoituresAsync();
        return Ok(voitures);
    }

    [HttpPost("upload-and-create")]
    public async Task<IActionResult> UploadAndCreate([FromForm] IFormFile file, [FromForm] StatutVoiture _statut, [FromForm] string _photo,
                                                     [FromForm] string _description, [FromForm] DateTime _anneeVoiture, [FromForm] int _MarqueId)
    {
        //Upload de l'image
        var imagePath = await _voitureService.UploadImageAsync(file);

        //Création de la voiture via notre service en lui fournissant les différents paramètres
        var createdVoiture = await _voitureService.CreateVoiture(_statut, _photo, _description, _anneeVoiture, _MarqueId);

        return StatusCode(201, createdVoiture);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVoiture(int id)
    {
        var success = await _voitureService.DeleteVoiture(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}