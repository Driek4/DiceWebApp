using Microsoft.AspNetCore.Mvc;
using DiceWebApp.Models;

namespace DiceWebApp.Controllers
{
    public class DiceController : Controller
    {
        private static DiceManager diceManager = new DiceManager();

        public IActionResult index()
        {
            return View(diceManager);
        }

        [HttpPost]
        public IActionResult add()
        {
            diceManager.AddDice();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Roll()
        {
            diceManager.RollAll();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int index)
        {
            diceManager.RemoveDice(index);
            return RedirectToAction("Index");
        }
    }
}
