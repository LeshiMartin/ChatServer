using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApplicationLayer.Dtos.ChatServerDtos;
public sealed record SendMessageDto ( Guid FromId, Guid ToId, string Message );


