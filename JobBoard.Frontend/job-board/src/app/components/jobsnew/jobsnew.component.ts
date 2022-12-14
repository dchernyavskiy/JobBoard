import { Component, OnInit } from "@angular/core";
import { JobService } from "src/app/services/job.service";
import {
  Client,
  GetJobsQuery,
  JobLookupDto,
  Location,
  CategoryLookupDto,
  EmployerLookupDto,
} from "src/app/api/api";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: "app-jobsnew",
  templateUrl: "./jobsnew.component.html",
  styleUrls: ["./jobsnew.component.scss"],
})
export class JobsnewComponent implements OnInit {
  public jobs: JobLookupDto[];
  public body: GetJobsQuery;
  public currentPage: number = 1;
  public pageCount: number;
  public categories: CategoryLookupDto[];
  public locations: Location[];
  public employers: EmployerLookupDto[];
  public searchString: string;
  public resultCount: number = 0;
  public years: number[] = [5, 4, 3, 2, 1];
  public sortString: string;
  public sortBy: string[] = ["Name", "Salary", "Experience"];
  public selected = this.sortBy[2];

  constructor(
    public client: Client,
    public jobService: JobService,
    public route: ActivatedRoute
  ) {
    this.route.queryParams.subscribe((res) => {
      this.initializeBody();
      this.getJobs();
    });
  }

  ngOnInit(): void {
    this.client.getAllGET("1").subscribe((res) => {
      this.categories = res.categories;
    });

    this.client.getAllGET3("1").subscribe((res) => {
      this.locations = res.locations;
    });

    this.client.getAllGET2("1").subscribe((res) => {
      this.employers = res.employers;
    });
  }

  getJobs() {
    this.client.getAllPOST("1", this.body).subscribe((result) => {
      this.jobs = result.jobs;
      this.resultCount = result.resultCount;
      this.pageCount = result.pageCount;
    });
  }

  initializeBody() {
    this.body = {
      filters: {
        keyWord: "",
        categoryIds: null,
        locationIds: null,
        salaryStart: 0,
        salaryEnd: 0,
        emloyerIds: null,
        experiences: null,
      },
      pagging: {
        count: 12,
        page: 1,
      },
      sort: {
        sortByName: false,
        sortBySalary: false,
        sortByExpirience: false,
        isAscending: true,
      },
    };
  }

  sort(sortBy: string) {
    console.log(sortBy);
    switch (sortBy) {
      case "Name":
        this.body.sort.sortByName = true;
        this.body.sort.sortBySalary = false;
        this.body.sort.sortByExpirience = false;
        break;
      case "Salary":
        this.body.sort.sortByName = false;
        this.body.sort.sortBySalary = true;
        this.body.sort.sortByExpirience = false;
        break;
      case "Experience":
        this.body.sort.sortByName = false;
        this.body.sort.sortBySalary = false;
        this.body.sort.sortByExpirience = true;
        break;
    }
    this.body.sort.isAscending = !this.body.sort.isAscending;

    this.getJobs();
  }

  range(count: number): number[] {
    let arr = [];
    for (let i = 1; i <= count; i++) {
      arr.push(i);
    }
    return arr;
  }

  categoryFilter(id: string) {
    if (this.body.filters.categoryIds == null) {
      this.body.filters.categoryIds = [];
      this.body.filters.categoryIds.push(id);
    } else if (this.body.filters.categoryIds.indexOf(id) >= 0) {
      let i = this.body.filters.categoryIds.indexOf(id);
      this.body.filters.categoryIds.splice(i, 1);
    } else {
      this.body.filters.categoryIds.push(id);
    }
    this.getJobs();
  }

  filterLocation(id: string) {
    if (this.body.filters.locationIds == null) {
      this.body.filters.locationIds = [];
      this.body.filters.locationIds.push(id);
    } else if (this.body.filters.locationIds.indexOf(id) >= 0) {
      let i = this.body.filters.locationIds.indexOf(id);
      this.body.filters.locationIds.splice(i, 1);
    } else {
      this.body.filters.locationIds.push(id);
    }
    this.getJobs();
  }

  filterEmployer(id: string) {
    if (this.body.filters.emloyerIds == null) {
      this.body.filters.emloyerIds = [];
      this.body.filters.emloyerIds.push(id);
    } else if (this.body.filters.emloyerIds.indexOf(id) >= 0) {
      let i = this.body.filters.emloyerIds.indexOf(id);
      this.body.filters.emloyerIds.splice(i, 1);
    } else {
      this.body.filters.emloyerIds.push(id);
    }
    this.getJobs();
  }

  setSalary() {
    return new Promise((res) => {
      setTimeout(res, 3000);
      this.getJobs();
    });
  }

  setExperience(year: number) {
    if (this.body.filters.experiences == null) {
      this.body.filters.experiences = [];
      this.body.filters.experiences.push(year);
    } else if (this.body.filters.experiences.indexOf(year) >= 0) {
      let i = this.body.filters.experiences.indexOf(year);
      this.body.filters.experiences.splice(i, 1);
    } else {
      this.body.filters.experiences.push(year);
    }
    this.getJobs();
  }

  setPage(page: number) {
    this.currentPage = page;
    this.body.pagging.page = page;
    this.getJobs();
  }

  setSearch() {
    this.getJobs();
  }

  reset() {
    var cbs = document.getElementsByTagName("input");
    for (let i = 0; i < cbs.length; i++) {
      if (cbs[i].type == "checkbox") {
        cbs[i].checked = false;
      }
    }
    this.initializeBody();
    this.getJobs();
  }
}
