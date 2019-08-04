using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShareALittle.Data;
using ShareALittle.Models;
using ShareALittle.Models.ProductViewModels;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Net.Mime;

namespace ShareALittle
{
    public class ProductsController : Controller
    {
        private readonly ShareALittleContext _context;

        public ProductsController(ShareALittleContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var shareALittleContext = _context.Product.Include(p => p.Category);
            return View(await shareALittleContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var pro = new Product()
                {
                    ProductName = model.ProductName,
                    ProductPrice = model.ProductPrice,
                    ProductDescription = model.ProductDescription,
                    CategoryID = model.CategoryID,
                    ContactDetail = model.ContactDetail,
                    OwnerID = userId,
                    IsBooked = false
                };
                using (var memoryStream = new MemoryStream())
                {
                    await model.ProductImage.CopyToAsync(memoryStream);
                    pro.ProductImage = memoryStream.ToArray();
                    
                }
                _context.Add(pro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName", model.CategoryID);
            return View(model);
        }

        // GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditProductViewModel model)
        {
            if (id != model.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var pro = await _context.Product.SingleOrDefaultAsync(m => m.ProductID == id);
                    
                        
                  
                    using (var memoryStream = new MemoryStream())
                    {
                        if (model.ProductImage != null)
                        {
                            await model.ProductImage.CopyToAsync(memoryStream);
                            pro.ProductImage = memoryStream.ToArray();
                        }
                        pro.ProductName = model.ProductName;
                        pro.ProductPrice = model.ProductPrice;
                        pro.ProductDescription = model.ProductDescription;
                        pro.CategoryID = model.CategoryID;
                        pro.ContactDetail = model.ContactDetail;
                    }

                    _context.Update(pro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(model.ProductID))
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
            ViewData["CategoryID"] = new SelectList(_context.Category, "CategoryID", "CategoryName", model.CategoryID);
            return View(model);
        }

        // GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.SingleOrDefaultAsync(m => m.ProductID == id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductID == id);
        }

        [Authorize]
        public async Task<IActionResult> Book(string Email, string productid)
        {
           
            int id = Int32.Parse(productid);
            if (ModelState.IsValid)
            {
                try
                {
                    var pro = await _context.Product.SingleOrDefaultAsync(m => m.ProductID == id);

                    using (var memoryStream = new MemoryStream())
                    {
                        pro.IsBooked = true;
                    }

                    _context.Update(pro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            //SendEmail(Email, ContactDetail);

            var shareALittleContext = _context.Product.Include(p => p.Category);
            return View(await shareALittleContext.ToListAsync());

        }

        [Authorize]
        public string SendEmail(string Email, string ContactDetail)
        {
            try
            {
                var credentials = new NetworkCredential("sharealittlebus@gmail.com", "P@ssw0rd987");

                var mail = new MailMessage()
                {
                    From = new MailAddress("sharealittlebus@gmail.com"),
                    Subject = "Thank you for your odering",
                };

                mail.IsBodyHtml = true;
                mail.To.Add(new MailAddress(Email));

                string html = "<!DOCTYPE html> <html> <style> .container {   padding: 20px; } </style> <body>  <div class='container'> <p>Dear customer,</p> </div>   <div class='container'>      <p>Here we are providing the contact details of the owner.</p>   </div>    <div class='container' style='background-color:white'>     <p>" + ContactDetail + "</p>   </div>    <div class='container'>     <p>If you find any trouble to contact the owner, let us know, we are happy to help you.</p>     <p>Our contact email: sharealittlebus@gmail.com</p>   </div>   <div class='container'>   <p>Sincerely</p>   <p>Share A Little Team</p>     </div> </body> </html>";
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, new ContentType("text/html"));
                mail.AlternateViews.Add(htmlView);

                var client = new SmtpClient()
                {
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(mail);

                return "Email Sent Successfully!";
            }
            catch (System.Exception e)
            {
                return e.Message;
            }

        }
    }
}
