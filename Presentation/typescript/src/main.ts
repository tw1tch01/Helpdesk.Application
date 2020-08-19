import { ITicketService } from './lib/services/tickets/ITicketsService';
import { TicketsService } from './lib/services/tickets/TicketsService';
import { HelpdeskClient } from './lib/common/HelpdeskClient';
import 'reflect-metadata';
import { container } from 'tsyringe';

container.register(nameof<HelpdeskClient>(), { useClass: HelpdeskClient });
container.register(nameof<ITicketService>(), { useClass: TicketsService });

const ticketService = container.resolve(TicketsService);
