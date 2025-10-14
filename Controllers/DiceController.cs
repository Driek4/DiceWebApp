using Microsoft.AspNetCore.Mvc;
using DiceWebApp.Models;
using System.Text.Json;

namespace DiceWebApp.Controllers
{
    public class DiceController : Controller
    {
        private DiceManager GetDiceManager()
        {
            var data = HttpContext.Session.GetString("DiceManager");

            if (string.IsNullOrEmpty(data))
                return new DiceManager(); // first-time user or no data yet

            var manager = JsonSerializer.Deserialize<DiceManager>(data);

            return manager ?? new DiceManager(); // fallback if deserialization fails
        }

        private void SaveDiceManager(DiceManager manager)
        {
            HttpContext.Session.SetString("DiceManager", JsonSerializer.Serialize(manager));
        }

        public IActionResult index()
        {
            var diceManager = GetDiceManager();
            return View(diceManager);
        }

        [HttpPost]
        public IActionResult add(string type)
        {
            var diceManager = GetDiceManager();

            if (Enum.TryParse<DiceType>(type, out var diceType))
                diceManager.AddDice(diceType);
            else
                diceManager.AddDice(); //fallback

            SaveDiceManager(diceManager);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Move(int index, string zone)
        {
            var diceManager = GetDiceManager();

            if (index >= 0 && index < diceManager.DiceList.Count)
            {
                diceManager.DiceList[index].Location = zone;
                SaveDiceManager(diceManager);
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult Reset()
        {
            var diceManager = GetDiceManager();
            diceManager.DiceList.Clear();
            SaveDiceManager(diceManager);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RollJson()
        {
            var diceManager = GetDiceManager();
            diceManager.RollAll();
            SaveDiceManager(diceManager);

            return Json(diceManager.DiceList);
        }

        [HttpPost]
        public IActionResult Remove(int index)
        {
            var diceManager = GetDiceManager();
            diceManager.RemoveDice(index);
            SaveDiceManager(diceManager);

            return RedirectToAction("Index");
        }
    }
}
