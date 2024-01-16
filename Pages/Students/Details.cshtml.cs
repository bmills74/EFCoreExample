﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Uni.Data;
using Uni.Models;

namespace Uni.Pages.Students
{
    public class DetailsModel : PageModel
    {
        private readonly Uni.Data.SchoolContext _context;

        public DetailsModel(Uni.Data.SchoolContext context)
        {
            _context = context;
        }

        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = 
                await _context.Students
                .Include(x=> x.Enrollments)
                .ThenInclude(x=> x.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(x=> x.ID == id);


            if (student == null)
            {
                return NotFound();
            }
            else
            {
                Student = student;
            }
            return Page();
        }
    }
}
