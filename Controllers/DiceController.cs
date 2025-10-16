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
                return new DiceManager();

            var manager = JsonSerializer.Deserialize<DiceManager>(data);
            return manager ?? new DiceManager();
        }

        private void SaveDiceManager(DiceManager manager)
        {
            HttpContext.Session.SetString("DiceManager", JsonSerializer.Serialize(manager));
        }

        public IActionResult Index()
        {
            var diceManager = GetDiceManager();
            return View(diceManager);
        }

        [HttpPost]
        public IActionResult Add(string type)
        {
            var diceManager = GetDiceManager();

            var diceType = Enum.Parse<DiceType>(type, true);
            var newDie = diceManager.Add(diceType);
            SaveDiceManager(diceManager);

            return Json(new
            {
                index = diceManager.DiceList.Count - 1,
                value = newDie.Value,
                type = newDie.Type.ToString(),
                location = newDie.Location
            });
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

            return Json(new { success = true });
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

            return Ok();
        }
    }
}
