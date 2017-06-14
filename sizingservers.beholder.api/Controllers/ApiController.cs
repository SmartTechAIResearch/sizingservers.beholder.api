/*
 * 2017 Sizing Servers Lab
 * University College of West-Flanders, Department GKG
 * 
 */

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace sizingservers.beholder.api.Controllers {
    /// <summary>
    /// <para>Holds all actions related to this api. Use url/ap/{action}.</para> 
    /// <para>Data is represented as JSON.</para> 
    /// </summary>
    public class ApiController : Controller {
        private static DateTime _epochUtc = new DateTime(1970, 1, 1, 1, 1, 1, 1, DateTimeKind.Utc);

        /// <summary>
        /// Some sort of hack to get if authorization should be enabled or not (appsettings.json).
        /// </summary>
        public static bool Authorization { get; set; }

        /// <summary>
        /// GET "pong" if the api is reachable.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Ping() {
            return "pong";
        }
        /// <summary>
        /// GET all stored system informations.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        [HttpGet]
        public Models.SystemInformation[] List([FromQuery]string apiKey) {
            if (!Authorize(apiKey))
                return null;

            using (var db = new DBContext())
                return db.SystemInformations.ToArray();
        }
        /// <summary>
        /// To POST / store a new system information in the database or replace an existing one using the hostname.
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="systemInformation"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Report([FromQuery]string apiKey, [FromBody]Models.SystemInformation systemInformation) {
            if (!Authorize(apiKey))
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var db = new DBContext()) {
                systemInformation.timeStampInSecondsSinceEpochUtc = (long)(DateTime.UtcNow - _epochUtc).TotalSeconds;

                Models.SystemInformation toReplace = db.SystemInformations.Where(x => x.hostname == systemInformation.hostname).FirstOrDefault();

                if (toReplace == null)
                    db.SystemInformations.Add(systemInformation);
                else
                    db.Entry(toReplace).CurrentValues.SetValues(systemInformation);

                db.SaveChanges();
            }

            return CreatedAtAction("list", null); //Return a 201. Tell the client that the post did happen and were it can be requested.
        }
        /// <summary>
        /// <para>Cleans up old system informations so the database represents reality.</para>
        /// <para>PUT url/api/cleanolderthan?days=#</para>
        /// </summary>
        /// <param name="days"></param>   
        /// <param name="apiKey"></param>     
        /// <returns></returns>
        [HttpPut]
        [Produces("plain/text")]
        public IActionResult CleanOlderThan([FromQuery]int days, [FromQuery]string apiKey) {
            if (!Authorize(apiKey))
                return Unauthorized();

            if (!ModelState.IsValid && days < 1)
                return BadRequest("Given days should be an integer greater than 0.");

            using (var db = new DBContext()) {
                long timeStampPastInSecondsSinceEpochUtc = (long)(DateTime.UtcNow.AddDays(days * -1) - _epochUtc).TotalSeconds;

                IQueryable<Models.SystemInformation> toRemove = db.SystemInformations.Where(x => x.timeStampInSecondsSinceEpochUtc <= timeStampPastInSecondsSinceEpochUtc);

                if (toRemove.Count() != 0) {
                    db.SystemInformations.RemoveRange(toRemove);
                    db.SaveChanges();
                }
            }

            return NoContent(); //Http PUT response --> 200 OK or 204 NoContent. Latter equals done.
        }
        /// <summary>
        /// Clear (PUT) all system informations in the database.
        /// <param name="apiKey"></param>
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Clear([FromQuery]string apiKey) {
            if (!Authorize(apiKey))
                return Unauthorized();

            using (var db = new DBContext()) {
                db.SystemInformations.RemoveRange(db.SystemInformations);
                db.SaveChanges();
            }

            return NoContent(); //Http PUT response --> 200 OK or 204 NoContent. Latter equals done.
        }

        private bool Authorize(string apiKey) {
            if (!Authorization) return true;

            if (!string.IsNullOrEmpty(apiKey))
                using (var db = new DBContext())
                    if (db.APIKeys.Select(x => x.key == apiKey).Count() != 0)
                        return true;

            return false;
        }
    }
}
