using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using System;
using Newtonsoft.Json;
using MVCClient.Model;
using Microsoft.AspNetCore.Cors;
using System.Net.Http.Headers;

namespace MVCClient.Controllers
{
    public class EmployeeController : Controller
    {
        HttpClient _client;
        string _url;
        public EmployeeController(IConfiguration config)
        {
            var handler = new HttpClientHandler();
            _client = new HttpClient(handler);
            _url = config["BaseUrl"]!;
        }

        public async Task<IActionResult> Index()
        {
            return View(JsonConvert.DeserializeObject<List<Employee>>(await _client.GetStringAsync(_url))!.ToList());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Consume API
            var employee = JsonConvert.DeserializeObject<Employee>(await _client.GetStringAsync(_url + "/" + id));

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                // Consume API
                HttpClient client = _client;
                string url = _url;

                await client.PostAsJsonAsync<Employee>(url, employee);

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Consume API
            var employee = JsonConvert.DeserializeObject<Employee>(await _client.GetStringAsync(_url + "/" + id));

            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Consume API
                    await _client.PutAsJsonAsync<Employee>(_url + "/" + id, employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Consume API
            var employee = JsonConvert.DeserializeObject<Employee>(await _client.GetStringAsync(_url + "/" + id));

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.DeleteAsync(_url + "/" + id);

            return RedirectToAction(nameof(Index));
        }
    }
}
