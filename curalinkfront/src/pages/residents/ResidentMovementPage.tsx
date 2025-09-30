import ResidentMovement from "@/components/ResidentMovement";
import ResidentHeader from "@/components/ResidentHeader";

export default function ResidentMovementPage({ residentId }: { residentId: number }) {

    return (
        <div className="p-6">
            <ResidentHeader residentId={residentId} />

            <ResidentMovement residentId={residentId} />
        </div>
    );
}
