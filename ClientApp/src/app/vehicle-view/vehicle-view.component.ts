import { ProgressService } from './../services/progress.service';
import { VehicleService } from './../services/vehicle.service';
import { Vehicle } from './../models/vehicle';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PhotoService } from '../services/photo.service';

@Component({
  selector: 'app-vehicle-view',
  templateUrl: './vehicle-view.component.html',
  styleUrls: ['./vehicle-view.component.css']
})
export class VehicleViewComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;
  vehicle: any;
  vehicleId: number;
  photos: any;

  constructor(
    private vehicleService: VehicleService,
    private photoService: PhotoService,
    private progressService: ProgressService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    route.params.subscribe(p => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicle <= 0){
        router.navigate(['/vehicles']);
        return;
      }
    })
  }

  ngOnInit() {
    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(v => this.vehicle = v, err => {
        console.log(err);
      });

      this.getPhotos();
  }


  uploadPhoto() {  
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    this.progressService.uploadProgress
      .subscribe(progress => console.log(progress));
    
    this.photoService.upload(this.vehicleId,nativeElement.files[0])
      .subscribe(p => {
        this.photos.push(p);
      });


  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/']);
        });
    }
  }

  getPhotos(){
    this.photoService.getPhotos(this.vehicleId)
      .subscribe(p => {
        this.photos = p;
      })
  }

  redirect(){
    this.router.navigate(['/vehicles/edit/',this.vehicle.id]);
  }
}
