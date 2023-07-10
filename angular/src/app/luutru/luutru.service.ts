import { Injectable } from "@angular/core";
import { Country } from "../shared/Country.model";
@Injectable({
  providedIn: "root",
})
export class LuutruService {
  Diadiems: Country[] = [
    {
      name: "Afghanistan",
      code: "AF",
    },
    {
      name: "Bfghanistan",
      code: "BF",
    },
    {
      name: "Cfghanistan",
      code: "CF",
    },
    {
      name: "Dfghanistan",
      code: "DF",
    },
  ];

  constructor() {}
  getDiadiems(): Promise<Country[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve(this.Diadiems);
      }, 200); // Simulate a delay of 200 milliseconds
    });
  }
}
