import ResidentAusscheidung from "@/components/ResidentAusscheidung";

export default function ResidentAusscheidungPage({ residentId }: { residentId: number }) {
    return (
        <div className="p-4">
            <ResidentAusscheidung residentId={residentId} />
        </div>
    );
}

