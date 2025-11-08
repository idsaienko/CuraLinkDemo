import { useParams } from "react-router-dom";
import { useResident } from "../hooks/useResident";
import { useEffect, useState } from "react";
import api from "../api/axiosConfig";
import type { MealSchedule } from "../types";

export default function ResidentNutrition() {
    const { residentId } = useParams<{ residentId: string }>();
    const { resident, loading } = useResident(residentId);
    const [meals, setMeals] = useState<MealSchedule[]>([]);
    const [mealsLoading, setMealsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        console.log("useEffect triggered, residentId:", residentId);

        if (!residentId) {
            console.log("No residentId, returning");
            return;
        }

        const url = `/api/MealSchedule/resident/${residentId}`;
        console.log("Fetching from URL:", url);

        api
            .get<MealSchedule[]>(url)
            .then((res) => {
                if (Array.isArray(res.data)) {
                    console.log("Setting meals to:", res.data);
                    setMeals(res.data);
                } else {
                    console.error("Expected array but got:", typeof res.data, res.data);
                    setError("Ungültiges Datenformat");
                }
            })
            .catch((err) => {
                console.error("ERROR - Full error:", err);
                console.error("ERROR - Error message:", err.message);
                console.error("ERROR - Error response:", err.response);
                setError(`Fehler: ${err.message}`);
            })
            .finally(() => {
                console.log("Request completed, setting mealsLoading to false");
                setMealsLoading(false);
            });
    }, [residentId]);

    if (loading || mealsLoading) return <p>Lädt...</p>;
    if (!resident) return <p>Kein Bewohner gefunden</p>;
    if (error) return <p style={{ color: 'red' }}>{error}</p>;


    return (
        <div className="NutritionPageWrap">
            <h2>Ernährung</h2>
            <div className="DataWrap">
                {meals.length > 0 ? (
                    meals.map((meal) => (
                        <div key={meal.id} className="border rounded p-3 mb-3 shadow MealWrap">
                            <div className="BlockTop">
                                <p>{meal.mealType} - {new Date(meal.mealTime).toLocaleTimeString()}</p>
                            </div>
                            <p className="Comments">{meal.comments || 'Keine Kommentare'}</p>
                        </div>
                    ))
                ) : (
                    <p>Keine Ernährungsdaten verfügbar</p>
                )}
            </div>
        </div>
    );
}