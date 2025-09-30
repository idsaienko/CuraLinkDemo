import { useEffect, useState } from "react";
import { getMealSchedules } from "@/services/mealService";

interface MealSchedule {
    id: number;
    mealName: string;
    mealTime: string;
    mealType: string;
    comments: string;
}

export default function ResidentNutrition({ residentId }: { residentId: number }) {
    const [meals, setMeals] = useState<MealSchedule[]>([]);

    useEffect(() => {
        getMealSchedules(residentId).then(setMeals);
    }, [residentId]);

    return (
        <div
            style={{
                color:"black"
            }}>
            <h2 className="text-xl font-bold mb-4">Ernährung</h2>
            {meals.map((meal) => (
                <div key={meal.id} className="border rounded p-3 mb-3 shadow">
                    <p><strong>{meal.mealName}</strong> – {new Date(meal.mealTime).toLocaleTimeString()}</p>
                    <p>Typ: {meal.mealType}</p>
                    <p>Kommentar: {meal.comments}</p>
                </div>
            ))}
        </div>
    );
}
