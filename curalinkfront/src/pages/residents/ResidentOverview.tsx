export default function ResidentOverview() {
    return (
        <div className="space-y-6"
            style={{
                color:"black"
            } }>
            <div className="flex items-center gap-6 bg-white p-6 rounded-xl shadow">
                <img
                    src="/resident.jpg"
                    alt="resident"
                    className="w-24 h-24 rounded-full object-cover"
                />
                <div>
                    <h2 className="text-2xl font-bold">Max Mustermann</h2>
                    <p className="text-gray-600">Zimmer 123 – Erdgeschoss</p>
                </div>
            </div>

            <div className="grid grid-cols-3 gap-6">
                <div className="bg-white p-4 rounded-xl shadow">
                    <h3 className="font-semibold mb-2">Allgemeine Infos</h3>
                    <ul className="text-sm text-gray-700 space-y-1">
                        <li>Zimmer: 12A</li>
                        <li>Diaet: Vegetarisch</li>
                        <li>Kontakt: +49 123 456789</li>
                    </ul>
                </div>

                <div className="bg-white p-4 rounded-xl shadow">
                    <h3 className="font-semibold mb-2">Tagesplan</h3>
                    <ul className="space-y-2">
                        <li className="flex items-center gap-2">
                            <span className="w-3 h-3 rounded-full bg-green-500"></span>
                            08:00 Frühstück – erledigt
                        </li>
                        <li className="flex items-center gap-2">
                            <span className="w-3 h-3 rounded-full bg-yellow-400"></span>
                            12:00 Mittagessen – ausstehend
                        </li>
                    </ul>
                </div>

                <div className="bg-white p-4 rounded-xl shadow">
                    <h3 className="font-semibold mb-2">Letzte Aktivitäten</h3>
                    <ul className="text-sm space-y-1">
                        <li>Gestern: Schmerzmittel verabreicht</li>
                        <li>Gestern: Spaziergang im Garten</li>
                    </ul>
                </div>
            </div>
        </div>
    );
}
