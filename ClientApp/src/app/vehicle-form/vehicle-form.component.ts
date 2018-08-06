import { VehicleService } from './../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes;
  models;
  features;
  vehicle: any = {}

  constructor(private vehicleService: VehicleService) { }

  ngOnInit() {
    this.vehicleService.getMakes().subscribe(makes => 
      this.makes = makes);
    
      this.vehicleService.getFeatures().subscribe(features => 
       this.features = features);
  }

  OnMakeChange(){
    var selectedMakes = this.makes.find( x => x.id = this.vehicle.make);
    this.models = selectedMakes ? selectedMakes.models : [];
  }

}
