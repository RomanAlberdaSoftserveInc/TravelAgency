CREATE VIEW [dbo].[viewTour] AS
SELECT
    t.id,
    t.country,
    t.city,
    t.includedInsurance,
    t.createdAt,
    t.updatedAt,
    tt.id as tourTypeId,
    tt.type as tourType,
    h.id as hotelId,
    h.name,
    h.stars,
    h.country as hotelCountry,
    h.city as hotelCity,
    h.address,
    h.description,
    h.pricePerDay,
    h.eatingType,
    trp.id as transportationId,
    trp.arrivalLocation,
    trp.depatureLocation,
    trp.arrivalTime,
    trp.depatureTime,
    trp.pricePerPerson,
    transp.id as transportId,
    transp.model,
    transp.type,
    transp.number
from
    tblTour t
    LEFT JOIN tblTourType tt ON t.tourTypeId = tt.id
    LEFT JOIN tblHotel h ON h.id = t.hotelId
    LEFT JOIN tblTourTransportation ttrp ON t.id = ttrp.tourId
    LEFT JOIN tblTransportation trp ON ttrp.transportation_id = trp.id
    LEFT JOIN tblTransport transp ON trp.transportId = transp.id