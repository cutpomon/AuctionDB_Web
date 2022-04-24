using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EFCoreFirstDB;
using EFCoreFirstDB.Models;

namespace EFCoreFirstDB.Web.Controllers
{
    public class LotsController : Controller
    {
        private readonly AuctionContext _context;

        public LotsController(AuctionContext context)
        {
            _context = context;
        }

        // GET: Lots
        public async Task<IActionResult> Index()
        {
            var auctionContext = _context.Lots.Include(l => l.Auction).Include(l => l.Customer).Include(l => l.Item).Include(l => l.Seller);
            return View(await auctionContext.ToListAsync());
        }

        // GET: Lots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots
                .Include(l => l.Auction)
                .Include(l => l.Customer)
                .Include(l => l.Item)
                .Include(l => l.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        // GET: Lots/Create
        public IActionResult Create()
        {
            ViewData["AuctionId"] = new SelectList(_context.Auctions, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.Applicants, "Id", "Name");
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name");
            ViewData["SellerId"] = new SelectList(_context.Applicants, "Id", "Name");
            return View();
        }

        // POST: Lots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AuctionId,SellerId,CustomerId,ItemId,StartPrice,BuyPrice")] Lot lot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuctionId"] = new SelectList(_context.Auctions, "Id", "Name", lot.AuctionId);
            ViewData["CustomerId"] = new SelectList(_context.Applicants, "Name", "Id", lot.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", lot.ItemId);
            ViewData["SellerId"] = new SelectList(_context.Applicants, "Id", "Name", lot.SellerId);
            return View(lot);
        }

        // GET: Lots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
            {
                return NotFound();
            }
            ViewData["AuctionId"] = new SelectList(_context.Auctions, "Id", "Name", lot.AuctionId);
            ViewData["CustomerId"] = new SelectList(_context.Applicants, "Id", "Name", lot.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", lot.ItemId);
            ViewData["SellerId"] = new SelectList(_context.Applicants, "Id", "Name", lot.SellerId);
            return View(lot);
        }

        // POST: Lots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AuctionId,SellerId,CustomerId,ItemId,StartPrice,BuyPrice")] Lot lot)
        {
            if (id != lot.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotExists(lot.Id))
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
            ViewData["AuctionId"] = new SelectList(_context.Auctions, "Id", "Name", lot.AuctionId);
            ViewData["CustomerId"] = new SelectList(_context.Applicants, "Id", "Name", lot.CustomerId);
            ViewData["ItemId"] = new SelectList(_context.Items, "Id", "Name", lot.ItemId);
            ViewData["SellerId"] = new SelectList(_context.Applicants, "Id", "Name", lot.SellerId);
            return View(lot);
        }

        // GET: Lots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots
                .Include(l => l.Auction)
                .Include(l => l.Customer)
                .Include(l => l.Item)
                .Include(l => l.Seller)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        // POST: Lots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lot = await _context.Lots.FindAsync(id);
            _context.Lots.Remove(lot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotExists(int id)
        {
            return _context.Lots.Any(e => e.Id == id);
        }
    }
}
