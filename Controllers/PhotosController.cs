using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VehicleDealer.ApplicationModels;
using VehicleDealer.Persistence.DataAbstraction.Interfaces;
using VehicleDealer.Persistence.DatabaseModel;
using VehicleDealer.Persistence.Repositories.Interfaces;
using VehicleDealer.Settings;

namespace VehicleDealer.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
        private readonly IPhotoRepository repository;
        public PhotosController(IHostingEnvironment host, IVehicleRepository vehicleRepository, IPhotoRepository repository, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
        {
            this.repository = repository;
            this.photoSettings = options.Value;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.vehicleRepository = vehicleRepository;
            this.host = host;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await vehicleRepository.GetVehicle(vehicleId);
            if (vehicle == null)
                return NotFound();

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("File too big");
            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("File format not supported");

            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);

            await unitOfWork.SaveAsync();

            return Ok(mapper.Map<Photo, PhotoModel>(photo));

        }

        [HttpGet]
        public async Task<IEnumerable<PhotoModel>> GetPhotosOfVehicle(int vehicleId)
        {
            var photos = await this.repository.GetPhotosOfVehicleAsync(vehicleId);

            return photos.Select(p => mapper.Map<Photo, PhotoModel>(p));
        }

        [HttpDelete("/api/vehicles/photos/{photoId}")]
        public async Task<IActionResult> RemovePhoto(int photoId)
        {
            var photo = await this.repository.GetPhoto(photoId);
            this.repository.RemovePhoto(photo);

            var uploadFolderPath = Path.Combine(host.WebRootPath, "uploads");
            var filePath = Path.Combine(uploadFolderPath, photo.FileName);
                      
            if(System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            await unitOfWork.SaveAsync();
            return Ok("File succesfully deleted");
        }
        
    }
}