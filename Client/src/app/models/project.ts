export interface Project {
    id: number;
    projectNumber: number;
    name: string;
    customer: string;
    status: string;
    startDate: Date;
    endDate: Date;
    selectedEmployeeId: number[];
}