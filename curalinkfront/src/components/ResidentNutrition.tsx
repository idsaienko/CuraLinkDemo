import { useParams } from "react-router-dom";
import { useResident } from "../hooks/useResident";
import { useEffect, useState } from "react";
import api from "../api/axiosConfig";
import ResidentHeader from "./ResidentHeader";
import type { MealSchedule } from "../types";

export default function ResidentNutrition() {
    const { residentId } = useParams<{ residentId: string }>();
    const { resident, loading } = useResident(residentId);
    const [meals, setMeals] = useState<MealSchedule[]>([]);
    const [mealsLoading, setMealsLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!residentId) return;

        console.log("Fetching meals for resident:", residentId);
        console.log("API instance baseURL:", api.defaults.baseURL);
        const fullUrl = `${api.defaults.baseURL}/api/MealSchedule/resident/${residentId}`;
        console.log("Full URL will be:", fullUrl);

        api
            .get(`/api/MealSchedule/resident/${residentId}`)
            .then((res) => {
                console.log("Raw API Response:", res);
                console.log("Response data:", res.data);
                console.log("Is array?", Array.isArray(res.data));
                console.log("Type:", typeof res.data);

                if (Array.isArray(res.data)) {
                    setMeals(res.data);
                } else {
                    console.error("Expected array but got:", res.data);
                    setError("Ungültiges Datenformat - keine Liste erhalten");
                }
            })
            .catch((err) => {
                console.error("Error fetching meals:", err);
                console.error("Error response:", err.response);
                setError(`Fehler: ${err.message}`);
            })
            .finally(() => setMealsLoading(false));
    }, [residentId]);

    if (loading || mealsLoading) return <p>Lädt...</p>;
    if (!resident) return <p>Kein Bewohner gefunden</p>;
    if (error) return <p style={{ color: 'red' }}>{error}</p>;

    return (
        <div style={{ color: "black" }}>
            <ResidentHeader />
            <h2 className="text-xl font-bold mb-4">Ernährung</h2>
            {meals.length > 0 ? (
                meals.map((meal) => (
                    <div key={meal.id} className="border rounded p-3 mb-3 shadow">
                        <p><strong>{meal.mealType}</strong> – {new Date(meal.mealTime).toLocaleTimeString()}</p>
                        <p>Kommentar: {meal.comments || 'Keine Kommentare'}</p>
                    </div>
                ))
            ) : (
                <p>Keine Ernährungsdaten verfügbar</p>
            )}
        </div>
    );
}