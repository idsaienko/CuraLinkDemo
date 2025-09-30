import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Layout from "./components/Layout";

import ResidentOverview from "./pages/residents/ResidentOverview";
import SidebarStaff from "./components/SidebarStaff";
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
                    <Route path="resident/:id/nutrition" element={<ResidentNutritionPage residentId={residentId} />} />
                    <Route path="resident/:id/movement" element={<ResidentMovementPage residentId={residentId} />} />
                    <Route path="resident/:id/ausscheidung" element={<ResidentAusscheidungPage residentId={residentId} />} />
                    <Route path="staff/dashboard" element={<StaffDashboard />} />
                    <Route
                        path="/staff"
                        element={
                            <div className="h-screen flex flex-col"
                                style={{
                                    position: "relative",
                                    top: "0px",
                                    left: "0px"
                                }}>
                                <div className="flex flex-1"
                                    style={{
                                        display: "flex"
                                    }}>
                                    <SidebarStaff />
                                    <StaffDashboard />
                                </div>
                            </div>
                        }
                    />
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
