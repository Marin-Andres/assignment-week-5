﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IndustryConnect_Week5_WebApi.Models;
using IndustryConnect_Week5_WebApi.Mappers;
using IndustryConnect_Week5_WebApi.Dtos;

namespace IndustryConnect_Week5_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IndustryConnectWeek2Context _context;

        public SaleController(IndustryConnectWeek2Context context)
        {
            _context = context;
        }

        // GET: api/Sale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSales()
        {
            var _sales = await _context.Sales.
                Include(p => p.Product).
                Include(c => c.Customer). 
                Include(st => st.Store).
                Select(s => SaleMapper.EntityToDto(s)).ToListAsync();
            
            if(_sales.Count > 0)
            {
                return Ok(_sales);
            }
            else
            {
                return BadRequest("There are no sales at the moment");
            }         
        }

        // GET: api/Sale/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetSale(int id)
        {
            var sale = await _context.Sales.Where(s => s.Id == id).
                Include(p => p.Product).
                Include(c => c.Customer).
                Include(st => st.Store).
                FirstOrDefaultAsync();

            if (sale == null)
            {
                return NotFound();
            }

            return SaleMapper.EntityToDto(sale);
        }

        // PUT: api/Sale/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, SaleDto sale)
        {
            if (id != sale.Id)
            {
                return BadRequest();
            }

            var entity = SaleMapper.DtoToEntity(sale);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(SaleMapper.EntityToDto(entity));
        }

        // POST: api/Sale
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(SaleDto sale)
        {
            var entity = SaleMapper.DtoToEntity(sale);

            _context.Sales.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
        }

        // DELETE: api/Sale/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
