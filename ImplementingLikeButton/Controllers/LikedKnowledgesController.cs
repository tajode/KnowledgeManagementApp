using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ImplementingLikeButton.Data;
using ImplementingLikeButton.Models;
using ImplementingLikeButton.ViewModels;
using System.Security.Claims;

namespace ImplementingLikeButton.Controllers
{
    public class LikedKnowledgesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikedKnowledgesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public JsonResult GetLikes(string knowledgeid, LikedKnowledge likedKnowledge)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.SingleOrDefault(u => u.Id == currentUserId);
                var capuserid = user?.Id.ToString();
                string like_yes = "Yes";
                string like_no = "No";

                var livelikes = _context.LikedKnowledge_Dbset?.AsEnumerable().Where(g => g.UserId?.ToString() == capuserid && g.KnowledgeId == knowledgeid).OrderBy(g => g.DateofCreation).FirstOrDefault();

                if (livelikes == null)
                {
                    likedKnowledge.UserId = capuserid;
                    likedKnowledge.KnowledgeId = knowledgeid.ToString();
                    likedKnowledge.LikeStatus = like_yes;
                    likedKnowledge.DateofCreation = DateTime.Now.ToUniversalTime();
                    likedKnowledge.DateofStatusChange = DateTime.Now.ToUniversalTime();

                    string clientIp;
                    if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                        clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                    else
                        clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                    likedKnowledge.IPAddress = clientIp;
                    _context.LikedKnowledge_Dbset?.Add(likedKnowledge);
                    _context.SaveChanges();
                }
                else
                {
                    var cap_likestatus = livelikes.LikeStatus;

                    if (cap_likestatus == like_yes)
                    {
                        livelikes.LikeStatus = like_no;
                        livelikes.DateofStatusChange = DateTime.Now.ToUniversalTime();

                        string clientIp;
                        if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                            clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                        else
                            clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                        livelikes.IPAddress = clientIp;
                        _context.SaveChanges();
                    }
                    else if (cap_likestatus == like_no)
                    {
                        livelikes.LikeStatus = like_yes;
                        livelikes.DateofStatusChange = DateTime.Now.ToUniversalTime();

                        string clientIp;
                        if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                            clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                        else
                            clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                        livelikes.IPAddress = clientIp;
                        _context.SaveChanges();
                    }
                }
                var response = "Success";
                return Json(response);
            }
            else
            {
                var response = "Create an account first";
                return Json(response);
            }
        }

        public JsonResult GetLikesCount(string countknowledgeid)
        {
            string like_yes = "Yes";
            var livelikes = _context.LikedKnowledge_Dbset?.Where(g => g.KnowledgeId == countknowledgeid && g.LikeStatus == like_yes);

            if (livelikes != null)
            {
                KnowledgeLikeCountViewModel likescount = new KnowledgeLikeCountViewModel
                {
                    LikeCount = livelikes.Count()
                };

                return Json(likescount);
            }
            else
            {
                KnowledgeLikeCountViewModel likescount = new KnowledgeLikeCountViewModel
                {
                    LikeCount = 0
                };

                return Json(likescount);
            }
        }

        public JsonResult GetLikesStatus(string statusknowledgeid)
        {
            string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _context.Users.SingleOrDefault(u => u.Id == currentUserId);
            var capuserid = user?.Id;

            var livelikestatus = _context.LikedKnowledge_Dbset?.SingleOrDefault(g => g.KnowledgeId == statusknowledgeid && g.UserId == capuserid);

            if (livelikestatus != null)
            {
                KnowledgeLikeStatusViewModel likesstatus = new KnowledgeLikeStatusViewModel
                {
                    LikeStatus = livelikestatus.LikeStatus
                };

                return Json(likesstatus);
            }
            else
            {
                KnowledgeLikeStatusViewModel likesstatus = new KnowledgeLikeStatusViewModel
                {
                    LikeStatus = "0"
                };

                return Json(likesstatus);
            }
        }

        private bool LikedKnowledgeExists(int id)
        {
            return (_context.LikedKnowledge_Dbset?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
