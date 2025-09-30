import ResidentNutrition from "@/components/ResidentNutrition";

export default function ResidentNutritionPage({ residentId }: { residentId: number }) {
    return (
        <div className="p-4">
            <ResidentNutrition residentId={residentId} />
        </div>
    );
}
