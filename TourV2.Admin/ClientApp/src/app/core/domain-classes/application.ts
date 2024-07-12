export class Application {
    id: number
    roomType: any
    closestAssociation: any
    surname: string
    name?: string
    fatherName: string
    motherName: string
    placeOfBirth: string
    swedenIdentificationNumber: string
    turkeyIdentificationNumber: string
    gender: any
    maritalStatus: any
    dateOfBirth: Date
    nationality: string
    passportNumber: string
    passportGivenDate: Date
    passportGivenPlace: string
    passportExpirationDate: Date
    address: string
    city: string
    country: string
    phoneNumber: string
    departureAirport: any
    landingAirport: any
    explanation: string
    passportPicture?: string
    headshotPicture: string
    roomTypeId: number
    closestAssociationId: number
    departureAirportId: number
    landingAirportId: number
    maritalStatusId: number
    genderId: number
    postCode: any
    periodId:number
}

export class Periods {
    id: number
    name: string
    isActive: boolean
    description:string
    data:any []

}