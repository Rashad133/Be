using Be.Models;
using System.ComponentModel.DataAnnotations;

namespace Be.Areas.Admin.ViewModels
{
    public class CreateTeamVM
    {
        [Required(ErrorMessage = "Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Is Required")]
        public string Surname { get; set; }


        [Required(ErrorMessage = "Is Required")]
        public IFormFile? Photo { get; set; }


        [Required(ErrorMessage = "Is Required")]
        public int PositionId { get; set; }
        public List<Position>? Positions {  get; set; }


        
        public string? TwitLink { get; set; }
        public string? FaceLink { get; set; }
        public string? PlusLink { get; set; }

    }
}
