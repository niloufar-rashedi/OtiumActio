using Microsoft.AspNetCore.Mvc;
using OtiumActio.Models;

namespace OtiumActio.Interfaces
{
    public interface IEditActivityController
    {
        IActionResult Edit(int id);
        IActionResult UpdateActivity(Activity activity);
    }
}