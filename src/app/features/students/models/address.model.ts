import { Student } from "./student.model";

export interface Address {
    addressId: number;
    street1: string;
    street2: string;
    city: string;
    state: string;
    zipCode: string;
    students: Student[];
}