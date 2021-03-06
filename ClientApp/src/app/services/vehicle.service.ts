import { Vehicle, SaveVehicle } from './../models/vehicle';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private http: HttpClient) { }

  getMakes() {
    return this.http.get<any[]>('/api/makes');
  }

  getFeatures() {
    return this.http.get<any[]>('/api/features');
  }

  create(vehicle) {
    return this.http.post("api/vehicles", vehicle);
  }

  update(vehicle: SaveVehicle): any {
    return this.http.put("api/vehicles/" + vehicle.id, vehicle);
  }

  getVehicle(id) {
    return this.http.get<Vehicle>('api/vehicles/' + id);
  }

  delete(id) {
    return this.http.delete<Vehicle>('api/vehicles/' + id);
  }

  getVehicles(filter) {
    return this.http.get<any>('api/vehicles' + '?' + this.toQueryString(filter));
  }

  toQueryString(obj) {   
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined)
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }    
    return parts.join('&');
  }
}
