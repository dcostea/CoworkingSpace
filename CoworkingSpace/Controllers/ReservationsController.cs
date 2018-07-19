using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoworkingSpace.Data;
using CoworkingSpace.Models;
using CoworkingSpace.Repository;

namespace CoworkingSpace.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationsController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            return View(await _reservationRepository.GetAllAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationRepository.FindAsync(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_reservationRepository.GetAllCustomers(), "CustomerId", "Name");
            ViewData["MembershipId"] = new SelectList(_reservationRepository.GetAllMemberships(), "MembershipId", "Title");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MembershipId,CustomerId,Details,StartDate,EndDate")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                await _reservationRepository.AddAsync(reservation);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_reservationRepository.GetAllCustomers(), "CustomerId", "Name", reservation.CustomerId);
            ViewData["MembershipId"] = new SelectList(_reservationRepository.GetAllMemberships(), "MembershipId", "Title", reservation.MembershipId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationRepository.FindAsync(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_reservationRepository.GetAllCustomers(), "CustomerId", "Name", reservation.CustomerId);
            ViewData["MembershipId"] = new SelectList(_reservationRepository.GetAllMemberships(), "MembershipId", "Title", reservation.MembershipId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,MembershipId,CustomerId,Details,StartDate,EndDate")] Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _reservationRepository.UpdateAsync(reservation);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_reservationRepository.ReservationExists(reservation.ReservationId))
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
            ViewData["CustomerId"] = new SelectList(_reservationRepository.GetAllCustomers(), "CustomerId", "Name", reservation.CustomerId);
            ViewData["MembershipId"] = new SelectList(_reservationRepository.GetAllMemberships(), "MembershipId", "Title", reservation.MembershipId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _reservationRepository.FindAsync(id.Value);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _reservationRepository.FindAsync(id);
            await _reservationRepository.RemoveAsync(reservation);
            return RedirectToAction(nameof(Index));
        }
    }
}
