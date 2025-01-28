using EMG_MED1000_BACKEND.Entities;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class ModeleController : ControllerBase
{
    private readonly ModeleService _modeleService;

    //Création d'un constructeur afin d'initialiser notre objet _modeleService d'accès à notre classe ModeleService
    public ModeleController(ModeleService modeleService)
    {
        _modeleService = modeleService;
    }

    //A partir de notre objet _marqueService, on aura acces a nos fonctionnalités métiers
    [HttpGet("modeles/{marqueId}")]
    public async Task<IActionResult> GetModelesByMarque(int marqueId)
    {
        var modeles = await _modeleService.GetAllModelesAsync(marqueId);

        if (modeles == null || !modeles.Any())
        {
            return NotFound("Aucun modèle trouvé pour cette marque");
        }

        return Ok(modeles);
    }

    [HttpPost]
    public async Task<IActionResult> CreateModele([FromBody] Modele modele)
    {
        //Création du modele via notre service en lui fournissant les différents paramètres
        var createdModele = await _modeleService.CreateModele(modele.nomModele, modele.anneeModele, modele.MarqId);

        return StatusCode(201, createdModele);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateModele(int id, [FromBody] Modele modele)
    {
        if (modele == null || id <=0)
            return BadRequest("Les données sont invalides");

        var success = await _modeleService.UpdateModele(id, modele.nomModele, modele.anneeModele, modele.MarqId);
        
        if (!success)
            return NotFound($"Aucun modele trouvé avec l'ID {id}");

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteModele(int id)
    {
        var success = await _modeleService.DeleteModele(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}