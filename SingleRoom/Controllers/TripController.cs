using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SingleRoom.Data;
using SingleRoom.Models;

namespace SingleRoom.Controllers
{
    [Authorize]
    public class TripController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TripController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trip
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trip.Where(x => x.ParticipantEmail == null).ToListAsync());
        }

        public async Task<IActionResult> Success(Trip trip)
        {
            return View(trip);
        }

        // GET: Trip/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Trip == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trip/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TripId,TripName,TravelAgencyName,StartingAirport,Destination,StartDate,EndDate,EmailAddress,Participant,ParticipantEmail")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                trip.TripId = Guid.NewGuid();
                trip.ParticipantEmail = null;
                _context.Add(trip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trip);
        }

        // GET: Trip/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Trip == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }

        // GET: Trip/Edit/5
        public async Task<IActionResult> EnrollToTrip(Guid? id)
        {
            if (id == null || _context.Trip == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            return View(trip);
        }



        [HttpPost]
        public async Task<IActionResult> EnrollToTrip(Guid id, [Bind("TripId,TripName,TravelAgencyName,StartingAirport,Destination,StartDate,EndDate,EmailAddress,ParticipantEmail")] Trip trip)
        {
            if (id != trip.TripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Success", trip);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.TripId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(trip);
        }


        // POST: Trip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("TripId,TripName,TravelAgencyName,StartingAirport,Destination,StartDate,EndDate,EmailAddress")] Trip trip)
        {
            if (id != trip.TripId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.TripId))
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
            return View(trip);
        }

        // GET: Trip/Delete/5
        [Authorize(Roles = ("Admin"))]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Trip == null)
            {
                return NotFound();
            }

            var trip = await _context.Trip
                .FirstOrDefaultAsync(m => m.TripId == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Trip == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TripViewModel'  is null.");
            }
            var trip = await _context.Trip.FindAsync(id);
            if (trip != null)
            {
                _context.Trip.Remove(trip);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(Guid id)
        {
            return _context.Trip.Any(e => e.TripId == id);
        }
    }
}
