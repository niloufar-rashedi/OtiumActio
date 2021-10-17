using Microsoft.AspNetCore.Mvc;
using OtiumActio.Domain.Activities;

namespace OtiumActio.Interfaces
{
    public interface IEditActivityController
    {
        IActionResult Edit(int id);
        IActionResult UpdateActivity(Activity activity);
    }
}