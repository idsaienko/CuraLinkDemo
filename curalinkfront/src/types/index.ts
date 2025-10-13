export interface Resident {
    id: number;
    fullName: string;
    roomNumber: string;
    photoUrl: string;
    location: string;
}

export interface MealSchedule {
    id: number;
    residentId: number;
    mealType: string;
    mealTime: string;
    comments?: string;
    mealName?: string;
}

export interface Ausscheidung {
    id: number;
    residentId: number;
    time: string;
    abstand: string;
    menge: string;
    konsistenz: string;
}

export interface ResidentMovement {
    id: number;
    residentId: number;
    movementTime: string;
    room: string;
    object: string;
    angle: number;
    notes?: string;
}