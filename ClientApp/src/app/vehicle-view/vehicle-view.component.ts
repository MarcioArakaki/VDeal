import { AuthService } from './../services/auth.service';
import { ProgressService } from './../services/progress.service';
import { VehicleService } from './../services/vehicle.service';
import { Vehicle } from './../models/vehicle';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PhotoService } from '../services/photo.service';
import { ToastrService } from 'ngx-toastr';

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
    private toastr: ToastrService,
    private vehicleService: VehicleService,
    private photoService: PhotoService,
    private progressService: ProgressService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {
    route.params.subscribe(p => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicle <= 0) {
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
    this.progressService.uploadProgress
      .subscribe(progress => console.log(progress));

    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    var file = nativeElement.files[0];
    nativeElement.value = '';
    this.photoService.upload(this.vehicleId, file)
      .subscribe(p => {
        this.photos.push(p);
      }, error => {
        debugger;
        this.toastr.error(error.error, 'Error');
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

  getPhotos() {
    this.photoService.getPhotos(this.vehicleId)
      .subscribe(p => {
        this.photos = p;
      })
  }

  redirect() {
    this.router.navigate(['/vehicles/edit/', this.vehicle.id]);
  }
}
