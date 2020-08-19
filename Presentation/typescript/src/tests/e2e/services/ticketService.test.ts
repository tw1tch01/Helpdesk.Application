import { ticketsService } from "../../../lib/services/ticketsService";

fixture("Ticket Service");
const config = require("./../../testconfig.json");

console.log(config);

describe("lookupAll", function () {
  it("should get all the tickets", async function () {
    let tickets = await ticketsService.lookupAll(null);
    expect(tickets).toReturn();
  });
});
