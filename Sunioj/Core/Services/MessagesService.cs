using AutoMapper;
using Sunioj.Core.Contracts.Services;
using Sunioj.Core.Contracts.UnitsOfWork;
using Sunioj.Core.Exceptions;
using Sunioj.Core.Resources.Messages;
using Sunioj.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sunioj.Core.Services
{
    public class MessagesService : IMessagesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MessagesService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageDTO> CreateAsync(MessageForCreationDTO messageForCreation)
        {
            var message = _mapper.Map<Message>(messageForCreation);
            message.CreatedAt = DateTime.Now;

            _unitOfWork.Messages.Add(message);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<MessageDTO>(message);
        }

        public async Task DeleteAsync(int id)
        {
            var message = await _unitOfWork.Messages.GetByIdAsync(id);

            if (message == null)
            {
                throw new NotFoundException($"Message {id} not found");
            }

            _unitOfWork.Messages.Remove(message);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<MessageDTO>> GetAllAsync()
        {
            var messages = await _unitOfWork.Messages.GetAllAsync();

            return _mapper.Map<IEnumerable<MessageDTO>>(messages);
        }

        public async Task<MessageDTO> GetByIdAsync(int id)
        {
            var message = await _unitOfWork.Messages.GetByIdAsync(id);

            if (message == null)
            {
                throw new NotFoundException($"Message {id} not found");
            }

            return _mapper.Map<MessageDTO>(message);
        }
    }
}
