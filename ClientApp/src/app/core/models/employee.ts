
export interface KeyValuePair {
    id: number;
    name: string;
}

export interface Employee {
    id: number;
    name: string;
    country: KeyValuePair;
    city: KeyValuePair;
    skills: KeyValuePair[]
    dateOfBirth: any;
    resume: string;
}
export interface SaveEmployee {
    id: number;
    name: string;
    cityId: number;
    countryId: number;
    skills: number[]
    dateOfBirth: any;
    resume: string;
}