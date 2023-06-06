using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee_Management.Models;

namespace Employee_Management.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        private readonly EmployeeDb _context;

        public EmployeeDetailsController(EmployeeDb context)
        {
            _context = context;
        }

        // GET: EmployeeDetails
        public async Task<IActionResult> Index()
        {
            return _context.EmployeeDetails != null ?
                        View( _context.EmployeeDetails.FromSqlRaw("exec DataOFEmployeeeDetails").ToList()):
                          Problem("Entity set 'EmployeeDb.EmployeeDetails'  is null.");


        }

        // GET: EmployeeDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeDetails == null)
            {
                return NotFound();
            }

            var employeeDetail = _context.EmployeeDetails.FromSqlRaw($"Exec EmployeeDataById @p0",id).ToList();
            var result = employeeDetail.Single();
            if (employeeDetail == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: EmployeeDetails/AddOrEdit

        // GET: EmployeeDetails/AddOrEdit/1
        public async Task<IActionResult> AddOrEdit(int id =0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                var employeeDetail = await _context.EmployeeDetails.FindAsync(id);
                if (employeeDetail == null)
                {
                    return NotFound();
                }
                return View(employeeDetail);
            }
        }

        // POST: EmployeeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,EmpFirstName,EmpLastName,EmpEmailId,EmpRole,Experience")] EmployeeDetail employeeDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDetail);
        }

        // GET: EmployeeDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeDetails == null)
            {
                return NotFound();
            }

            var employeeDetail = await _context.EmployeeDetails.FindAsync(id);
            if (employeeDetail == null)
            {
                return NotFound();
            }
            return View(employeeDetail);
        }

        // POST: EmployeeDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,EmpFirstName,EmpLastName,EmpEmailId,EmpRole,Experience")] EmployeeDetail employeeDetail)
        {
            if (id != employeeDetail.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDetailExists(employeeDetail.EmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDetail);
        }

        // GET: EmployeeDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeDetails == null)
            {
                return NotFound();
            }

            var employeeDetail = await _context.EmployeeDetails
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employeeDetail == null)
            {
                return NotFound();
            }

            return View(employeeDetail);
        }

        // POST: EmployeeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeDetails == null)
            {
                return Problem("Entity set 'EmployeeDb.EmployeeDetails'  is null.");
            }
            var employeeDetail = await _context.EmployeeDetails.FindAsync(id);
            if (employeeDetail != null)
            {
                _context.EmployeeDetails.Remove(employeeDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDetailExists(int id)
        {
          return (_context.EmployeeDetails?.Any(e => e.EmpId == id)).GetValueOrDefault();
        }
    }
}
