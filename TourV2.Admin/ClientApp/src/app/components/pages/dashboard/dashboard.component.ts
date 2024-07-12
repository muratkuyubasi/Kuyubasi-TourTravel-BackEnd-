import { Component, OnInit } from '@angular/core';
import { DashboardService } from './dashboard.service';
import { SecurityService } from '@core/security/security.service';
import { User } from '@core/domain-classes/user';
import { UserService } from '../../user/user.service';
import { SignalrService } from '@core/services/signalr.service';
import { BaseComponent } from 'src/app/base.component';
import { environment } from '@environments/environment';
import { ReservationService } from '../../reservation/reservation.service';
import { ActiveTourService } from '../../activetour/activetour.service';
import { UserAuth } from '@core/domain-classes/user-auth';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent extends BaseComponent implements OnInit {

  appUserAuth: UserAuth = null;
  totalTourCount = 0;
  totalActiveTourCount = 0;
  totalReservationCount = 0;
  totalNewReservationCount = 0;
  totalUserCount = 0;
  activeUserCount = 0;
  inactiveUserCount = 0;
  onlineUsers: User[];
  onlinerUsersCount: number = 0;
  recentlyRegisteredUsers: User[] = [];
  lastReservations:any[]=[]
  upcomingTours:any[]=[]


  constructor(
    private dashboardService:DashboardService,
    private securityService:SecurityService,
    private signalrService:SignalrService,
    private reservationService:ReservationService,
    private activeTourService:ActiveTourService,
    private userService:UserService
  ) {
    super()
   }

  ngOnInit(): void {
    this.setTopLogAndName()
    this.getTotalTourCount();
    this.getTotalActiveTourCount();
    this.getTotalReservationCount();
    this.getTotalNewReservationCount();
    this.getLastReservation()
    this.getUpcomingTours()
    this.getRecentlyRegisteredUsers();
    this.getRecentlyRegisteredUsers();
    this.getActiveUserCount();
    this.getInactiveUserCount();
    this.getTotalUserCount();
    this.getOnlineUsers();
  }

  getTotalTourCount() {
    this.sub$.sink = this.dashboardService.getTotalTourCount().subscribe((count: number) => this.totalTourCount = count);
  }

  getTotalActiveTourCount() {
    this.sub$.sink = this.dashboardService.getTotalActiveTourCount().subscribe((count: number) => this.totalActiveTourCount = count);
  }

  getTotalReservationCount() {
    this.sub$.sink = this.dashboardService.getTotalReservationCount().subscribe((count: number) => this.totalReservationCount = count);
  }

  getTotalNewReservationCount() {
    this.sub$.sink = this.dashboardService.getTotalNewReservationCount().subscribe((count: number) => this.totalNewReservationCount = count);
  }

  getLastReservation(){
    this.reservationService.getAllReservations().subscribe(resp=>{
      this.lastReservations = resp;
    })
  }

  getUpcomingTours(){
    this.activeTourService.getAllActiveTourByLang("tr").subscribe(resp=>{
      this.upcomingTours = resp;
    })
  }

  getOnlineUsers() {
    this.sub$.sink = this.signalrService.onlineUsers$.subscribe(c => {
      if (c) {
        this.sub$.sink = this.dashboardService.getOnlineUser()
          .subscribe((resp: User[]) => {
            this.onlineUsers = resp;
            this.onlineUsers.forEach(user => {
              if (user.profilePhoto) {
                user.profilePhoto = `${environment.apiUrl}${user.profilePhoto}`
              }
            })
          });
        this.onlinerUsersCount = c.length;
      } else {
        this.onlineUsers = [];
        this.onlinerUsersCount = 0;
      }
    })
  }

  getRecentlyRegisteredUsers() {
    this.sub$.sink = this.userService.getRecentlyRegisteredUsers().subscribe((users: User[]) => {
      this.recentlyRegisteredUsers = users;
    });
  }

  getActiveUserCount() {
    this.sub$.sink = this.dashboardService.getActiveUserCount().subscribe((count: number) => this.activeUserCount = count);
  }

  getInactiveUserCount() {
    this.sub$.sink = this.dashboardService.getInactiveUserCount().subscribe((count: number) => this.inactiveUserCount = count);
  }

  getTotalUserCount() {
    this.sub$.sink = this.dashboardService.getTotalUserCount().subscribe((count: number) => this.totalUserCount = count);
  }

  setTopLogAndName() {
    this.sub$.sink = this.securityService.securityObject$.subscribe(c => {
      if (c) {
        console.log(c)
        this.appUserAuth = c;
        if (this.appUserAuth.profilePhoto) {
          this.appUserAuth.profilePhoto = `${environment.apiUrl}${this.appUserAuth.profilePhoto}`
        }
      }
    })
  }

}
