import { Injectable } from '@angular/core';
import { Category, JobLookupDto, Location } from '../api/api';

@Injectable({
  providedIn: 'root'
})
export class JobService {
  public category1 : Category =  {
    id: "1",
    name: "category1"
  };

  public location1 : Location = {
    id: "1",
    city: "Kharkob"
  };
  
  public jobs : JobLookupDto[] = [
    {
      id: "1",
      name: "",
      location: this.location1,
      datePosted: new Date("11/01/2022"),
      employment: "string",
      shortDiscription: "",
      category: this.category1,
    },
    {
      id: "2",
      name: "",
      location: this.location1,
      datePosted: new Date("11/01/2022"),
      employment: "string",
      shortDiscription: "",
      category: this.category1,
    },
    {
      id: "3",
      name: "",
      location: this.location1,
      datePosted: new Date("11/01/2022"),
      employment: "string",
      shortDiscription: "",
      category: this.category1,
    },
    {
      id: "4",
      name: "",
      location: this.location1,
      datePosted: new Date("11/01/2022"),
      employment: "string",
      shortDiscription: "",
      category: this.category1,
    },
    {
      id: "5",
      name: "",
      location: this.location1,
      datePosted: new Date("11/01/2022"),
      employment: "string",
      shortDiscription: "",
      category: this.category1,
    },
    {
      id: "6",
      name: "",
      location: this.location1,
      datePosted: new Date("11/01/2022"),
      employment: "string",
      shortDiscription: "",
      category: this.category1,
    },
    {
      id: "7",
      name: "",
      location: this.location1,
      datePosted: new Date("11/01/2022"),
      employment: "string",
      shortDiscription: "",
      category: this.category1,
    },
    {
      id: "8",
      name: "",
      location: this.location1,
      datePosted: new Date("11/01/2022"),
      employment: "string",
      shortDiscription: "",
      category: this.category1,
    },
    {
      id: "9",
      name: "",
      location: this.location1,
      datePosted: new Date("11/01/2022"),
      employment: "string",
      shortDiscription: "",
      category: this.category1,
    },
    {
      id: "10",
      name: "",
      location: this.location1,
      datePosted: new Date("11/01/2022"),
      employment: "string",
      shortDiscription: "",
      category: this.category1,
    },
  ]/* ———————no problems?————————————
⠀⣞⢽⢪⢣⢣⢣⢫⡺⡵⣝⡮⣗⢷⢽⢽⢽⣮⡷⡽⣜⣜⢮⢺⣜⢷⢽⢝⡽⣝
⠸⡸⠜⠕⠕⠁⢁⢇⢏⢽⢺⣪⡳⡝⣎⣏⢯⢞⡿⣟⣷⣳⢯⡷⣽⢽⢯⣳⣫⠇
⠀⠀⢀⢀⢄⢬⢪⡪⡎⣆⡈⠚⠜⠕⠇⠗⠝⢕⢯⢫⣞⣯⣿⣻⡽⣏⢗⣗⠏⠀
⠀⠪⡪⡪⣪⢪⢺⢸⢢⢓⢆⢤⢀⠀⠀⠀⠀⠈⢊⢞⡾⣿⡯⣏⢮⠷⠁⠀⠀
⠀⠀⠀⠈⠊⠆⡃⠕⢕⢇⢇⢇⢇⢇⢏⢎⢎⢆⢄⠀⢑⣽⣿⢝⠲⠉⠀⠀⠀⠀
⠀⠀⠀⠀⠀⡿⠂⠠⠀⡇⢇⠕⢈⣀⠀⠁⠡⠣⡣⡫⣂⣿⠯⢪⠰⠂⠀⠀⠀⠀
⠀⠀⠀⠀⡦⡙⡂⢀⢤⢣⠣⡈⣾⡃⠠⠄⠀⡄⢱⣌⣶⢏⢊⠂⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⢝⡲⣜⡮⡏⢎⢌⢂⠙⠢⠐⢀⢘⢵⣽⣿⡿⠁⠁⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠨⣺⡺⡕⡕⡱⡑⡆⡕⡅⡕⡜⡼⢽⡻⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⣼⣳⣫⣾⣵⣗⡵⡱⡡⢣⢑⢕⢜⢕⡝⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⣴⣿⣾⣿⣿⣿⡿⡽⡑⢌⠪⡢⡣⣣⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⡟⡾⣿⢿⢿⢵⣽⣾⣼⣘⢸⢸⣞⡟⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
⠀⠀⠀⠀⠁⠇⠡⠩⡫⢿⣝⡻⡮⣒⢽⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀
—————————————————————————————
*/
  constructor() { }

  getAll() : JobLookupDto[] {
    return this.jobs;
  }

}
