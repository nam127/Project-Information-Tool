import { StatusEnum } from "../enums/status-enum";
import { Group } from "./group";

export interface Project {
    id: number;
    projectNumber: number;
    name: string;
    customer: string;
    status: StatusEnum;
    startDate: Date;
    endDate: Date;
    visas: number[];
}