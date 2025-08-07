﻿// using System.Collections.Generic;
// using System.Linq;
// using NeoServer.Data.Entities;
// using NeoServer.Data.Extensions;
//
// namespace NeoServer.Data.Parsers;
//
// public static class ItemEntityParser
// {
//     public static T ToPlayerItemEntity<T>(IItem item) where T : PlayerItemBaseEntity, new()
//     {
//         var itemModel = new T
//         {
//             ServerId = (short)item.Metadata.ServerId,
//             Amount = item is ICumulative cumulative ? cumulative.Amount : (short)1,
//             DecayTo = item.Decay?.DecaysTo,
//             DecayDuration = item.Decay?.Duration,
//             DecayElapsed = item.Decay?.Elapsed,
//             Charges = item is IChargeable chargeable ? chargeable.Charges : null,
//             Attributes = item.ExtractAllAttributes()
//         };
//
//         return itemModel;
//     }
//
//     public static IItem BuildContainer<T>(IContainer container, List<T> items, Location location,
//         IItemFactory itemFactory) where T : PlayerItemBaseEntity
//     {
//         if (items == null || items.Count == 0)
//             return container;
//
//         var childrenContainers = new Queue<(IContainer Container, int ContainerId)>();
//         childrenContainers.Enqueue((container, 0));
//
//         while (childrenContainers.TryDequeue(out var dequeuedContainer))
//         {
//             var containerItemsRecords = items.Where(x => x.ParentId == dequeuedContainer.ContainerId)
//                 .OrderByDescending(x => x.Id).ToList();
//
//             foreach (var itemRecord in containerItemsRecords)
//             {
//                 //todo: check this, if need pass Metadata to itemFactory.Create
//                 var item = itemFactory.Create((ushort)itemRecord.ServerId, location, null, null,
//                     itemRecord.GetAttributes(), itemRecord.GetCustomAttributes());
//
//                 if (item is ICumulative cumulativeItem && itemRecord.Amount > 1)
//                     cumulativeItem.Amount = (byte)itemRecord.Amount;
//
//                 dequeuedContainer.Container.AddItem(item);
//
//                 if (item is not IContainer childContainer)
//                     continue;
//
//                 childContainer.SetParent(dequeuedContainer.Container);
//                 childrenContainers.Enqueue((childContainer, itemRecord.ContainerId));
//             }
//         }
//
//         return container;
//     }
// }

