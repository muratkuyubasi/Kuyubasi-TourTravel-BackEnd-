import { Component, OnInit } from '@angular/core';
import { UntypedFormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Application } from '@core/domain-classes/application';
import { TranslationService } from '@core/services/translation.service';
import moment from 'moment';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/base.component';
import { ApplicationService } from '../../application.service';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-umrah-application-manage',
  templateUrl: './umrah-application-manage.component.html',
  styleUrls: ['./umrah-application-manage.component.css']
})
export class UmrahApplicationManageComponent extends BaseComponent implements OnInit {

  manageForm: UntypedFormGroup;
  isLoadingResults: boolean;
  candidate: Application;
  photoUrl: string = environment.apiUrl
  association: any[] = [];
  airports: any[] = [];
  roomType: any[] = []
  id: number;
  isLoadingButton: boolean = false;

  gender = [
    { id: 1, name: "KADIN" },
    { id: 2, name: "ERKEK" }
  ]

  maritalStatus = [
    { id: 1, name: "EVLİ" },
    { id: 2, name: "BEKAR" }
  ]

  constructor(
    private activeRoute: ActivatedRoute,
    private toastrService: ToastrService,
    private translationService: TranslationService,
    private formBuilder: FormBuilder,
    private applicationService: ApplicationService,

  ) {
    super();

  }


  ngOnInit(): void {
    this.createManageForm();
    this.sub$.sink = this.activeRoute.params.subscribe((params: { id: number }) => {
      this.id = params.id;
      if (this.id) {
        this.getUmrahCandidate(this.id);
        this.getAssociations();
        this.getAirport();
        this.getRoomType();

      }
    });
  }

  getAssociations() {
    this.applicationService.getAllAssociations().subscribe((data: any) => {
      this.association = data;
      const closestAssociationId = this.candidate?.closestAssociationId;
      const selectedClosestAssociationId = this.association.find(closestAssociation => closestAssociation.id === closestAssociationId);
      this.manageForm.patchValue({ closestAssociation: selectedClosestAssociationId.id })

    });
  }

  getAirport() {
    this.applicationService.getAllAirports().subscribe((data: any) => {
      this.airports = data;
      const departureAirportId = this.candidate?.departureAirportId;
      const landingAirportId = this.candidate?.landingAirportId;
      const selectedDepartureAirport = this.airports.find(airports => airports.id === departureAirportId);
      const selectedLandingAirport = this.airports.find(airport => airport.id === landingAirportId);
      this.manageForm.patchValue({ departureAirport: selectedDepartureAirport });
      this.manageForm.patchValue({ landingAirport: selectedLandingAirport });

    });
  }

  getRoomType() {
    this.applicationService.getAllRoomTypes().subscribe((data: any) => {
      this.roomType = data;
      const roomTypeId = this.candidate?.roomTypeId;
      const selectedRoomTypeId = this.roomType.find(roomType => roomType.id === roomTypeId);
      this.manageForm.patchValue({ roomType: selectedRoomTypeId });

    });
  }

  getUmrahCandidate(id: number) {
    this.sub$.sink = this.applicationService.getUmrahCandidatesById(id).subscribe(
      (data: any) => {
        this.candidate = data;
        const formatDateOfBirth = moment(data.dateOfBirth).format('yyyy-MM-DD');
        const formatPassportGiven = moment(data.passportGivenDate).format('yyy-MM-DD');
        const formatPassportExpiration = moment(data.passportExpirationDate).format('yyyy-MM-DD');
        const selectedGender = this.candidate.genderId;
        const selectedMaritalStatus = this.candidate.maritalStatusId;
        const selectedRoomType = this.candidate.roomTypeId;

        this.manageForm.patchValue(data);
        this.manageForm.patchValue({
          genderId: selectedGender,
          maritalStatusId: selectedMaritalStatus,
          roomTypeId: selectedRoomType,
          dateOfBirth: formatDateOfBirth,
          passportGivenDate: formatPassportGiven,
          passportExpirationDate: formatPassportExpiration,
        });


      },
      (error) => {
        this.handleHttpError(error);
      }
    );

  }

  createManageForm() {
    this.manageForm = this.formBuilder.group({
      id: [this.id],
      surname: ['', [Validators.required]],
      name: ['', [Validators.required]],
      fatherName: ['', [Validators.required]],
      motherName: ['', [Validators.required]],
      placeOfBirth: ['', [Validators.required]],
      swedenIdentificationNumber: ['', [Validators.required]],
      turkeyIdentificationNumber: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required]],
      nationality: ['', [Validators.required]],
      passportNumber: ['', [Validators.required]],
      passportGivenDate: ['', [Validators.required]],
      passportGivenPlace: ['', [Validators.required]],
      passportExpirationDate: ['', [Validators.required]],
      address: ['', [Validators.required]],
      city: ['', [Validators.required]],
      country: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      explanation: [''],
      passportPicture: [''],
      headshotPicture: [''],
      postCode: ['', [Validators.required]],
      roomTypeId: ['', [Validators.required]],
      closestAssociationId: ['', [Validators.required]],
      departureAirportId: ['', [Validators.required]],
      landingAirportId: ['', [Validators.required]],
      maritalStatusId: ['', [Validators.required]],
      genderId: ['', [Validators.required]]
    });
  }

  updateManageForm() {
    this.isLoadingButton = true;
    if (this.manageForm.valid) {
      const candidate = this.manageForm.value;

      this.sub$.sink = this.applicationService.updateUmrahCandidate(candidate).subscribe(() => {
        this.isLoadingButton = false;
        this.toastrService.success(this.translationService.getValue('UPDATED_SUCCESSFULLY'));
        this.getUmrahCandidate(this.id)
      }, error => {
        this.isLoadingButton = false;
        this.handleHttpError(error);
      });
    } else {
      this.isLoadingButton = false;
      this.manageForm.markAllAsTouched();
    }

  }

  handleHttpError(error: any): void {
    if (error && error.status === 409) {

      this.toastrService.error(this.translationService.getValue('Aynı kayıttan bulunmakta.'));
    } else {

      this.toastrService.error(this.translationService.getValue('Bilinmeyen Hata'));
    }
  }

  uploadFileEvent($event, type: any) {
    const selectedFile = $event.target.files[0];

    if (!selectedFile) {
      return;
    }

    const reader = new FileReader();

    reader.readAsDataURL(selectedFile);
    reader.onload = (_event) => {
      const file: any = {
        FormFile: selectedFile

      };



      const mimeType = selectedFile.type;

      if (!mimeType.match(/image\/*/)) {
        return;
      }

      const formData = new FormData();
      formData.append('FormFile', selectedFile);

      this.applicationService.addFile(formData)
        .subscribe(
          (resp: any) => {
            if (type == 'pass') {
              this.manageForm.patchValue({
                passportPicture: resp
              })
            }
            if (type == 'head') {
              this.manageForm.patchValue({
                headshotPicture: resp
              })
            }

          },
          (error: any) => {
            // console.error('Api çağrısında hata:', error);
          }
        );


    };


  }
}

