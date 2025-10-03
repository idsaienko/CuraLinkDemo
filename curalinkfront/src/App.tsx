import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Layout from "./components/Layout";

import ResidentOverview from "./pages/residents/ResidentOverview";
import StaffDashboard from "./pages/staff/StaffDashboard";
import ResidentAusscheidungPage from "./pages/residents/ResidentAusscheidungPage";
import ResidentNutritionPage from "./pages/residents/ResidentNutritionPage";
import ResidentMovementPage from "./pages/residents/ResidentMovementPage";

function App() {
    const residentId = 1;

    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Layout />}>
                    <Route index element={<Navigate to={`/resident/${residentId}/overview`} replace />} />

                    <Route path={`/resident/${residentId}/overview`} element={<ResidentOverview />} />
                    <Route path={`/resident/${residentId}/nutrition`} element={<ResidentNutritionPage residentId={residentId} />} />
                    <Route path={`/resident/${residentId}/movement`} element={<ResidentMovementPage residentId={residentId} />} />
                    <Route path={`/resident/${residentId}/excretion`} element={<ResidentAusscheidungPage residentId={residentId} />} />
                    <Route path="staff/dashboard" element={<StaffDashboard />} />

                    {/*<Route path="resident/:id/reports" element={<ResidentReportsPage />} />
                    <Route path="resident/:id/medications" element={<ResidentMedicationsPage />} />
                    <Route path="resident/:id/vitals" element={<ResidentVitalSignsPage />} />
                    <Route path="resident/:id/pain" element={<ResidentPainPage />} />
                    <Route path="resident/:id/documents" element={<ResidentDocumentsPage />} />
                    <Route path="resident/:id/appointments" element={<ResidentAppointmentsPage />} /> */}
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

export default App;
