import { Address } from "./address.model";

export interface Student {
    studentID: number;
    firstName: string;
    middleName: string;
    lastName: string;
    birthdate: Date;
    ssn: string;
    addresses: Address[];
}
