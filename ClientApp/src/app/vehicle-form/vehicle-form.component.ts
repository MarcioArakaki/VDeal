import { Vehicle } from './../models/vehicle';
import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../services/vehicle.service';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { forkJoin } from 'rxjs';
import { SaveVehicle } from '../models/vehicle';
import * as _ from  'underscore';


@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any[];
  models: any[];
  features: any[];
  vehicle: SaveVehicle = {
    id:0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: '',
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private vehicleService: VehicleService,
    private toastr: ToastrService,
    ) {
      route.params.subscribe(p => {
        this.vehicle.id = +p['id'];
      })
    }

  ngOnInit() {

    var sources : Observable<any>[] = [];

     sources.push(this.vehicleService.getMakes());
     sources.push(this.vehicleService.getFeatures());

    if(this.vehicle.id)    
      sources.push(this.vehicleService.getVehicle(this.vehicle.id));

        forkJoin(sources).subscribe(data => {
        this.makes = data[0];
        this.features = data[1];
        if (this.vehicle.id){
          this.setVehicle(data[2]);
          this.populateModels();
        }
          
    }, error => {
      if(error.status = 404)
        this.router.navigate(['/home']);
    });
  }

  private setVehicle(vehicle: Vehicle) {
    this.vehicle.id = vehicle.id;
    this.vehicle.makeId = vehicle.make.id;
    this.vehicle.modelId = vehicle.model.id;
    this.vehicle.isRegistered = vehicle.isRegistered;
    this.vehicle.contact = vehicle.contact;
    this.vehicle.features = _.pluck(vehicle.features,'id');   
  }

  onMakeChange(){
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels(){
    var selectedMake = this.makes.find(m => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onFeatureToggle(featureId,$event){
    if ($event.target.checked)
      this.vehicle.features.push(featureId);
    else{
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index,1);
    } 
  }

  submit(){
    if(this.vehicle.id){
      this.vehicleService.update(this.vehicle)
        .subscribe(x => {
          this.toastr.success('The vehicle was succesfully updated','Success');
        });
      }else{
        this.vehicleService.create(this.vehicle)
          .subscribe(x => console.log(x));
      }    
  }
}
